using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.AuthorizationResolution;

public class DefaultUserAccessTokenResolver(
    IUserAccessTokenStore tokenStore,
    IRefreshUserAccessToken? refresher,
    IRequestUserAccessToken? newTokenRequester,
    TimeSpan? expirationBuffer = null
    )
    : IResolveUserAccessToken
{
    private readonly IUserAccessTokenStore _tokenStore = tokenStore;
    private readonly IRefreshUserAccessToken? _refresher = refresher;
    private readonly IRequestUserAccessToken? _newTokenRequester = newTokenRequester;
    private readonly TimeSpan _expirationBuffer = expirationBuffer ?? TimeSpan.FromMinutes(5);
    private readonly ConcurrentDictionary<UserAccessTokenKey, SemaphoreSlim> _semaphores = new();

    public async ValueTask<UserAccessToken?> GetToken(UserAccessTokenKey key, CancellationToken ct = default)
    {
        SemaphoreSlim semaphore = _semaphores.GetOrAdd(key, _ => new SemaphoreSlim(1, 1)); // Need to verify that HashCode of these keys is dependent on property values and not pointers.
        await semaphore.WaitAsync(ct);
        try
        {
            if (await _tokenStore.GetTokenDetails(key, ct) is not UserAccessTokenDetails storedDetails)
                return await RequestNewToken(key, ct); // returns null.

            if (storedDetails.ExpiresAt > DateTimeOffset.UtcNow + _expirationBuffer)
                return storedDetails.AccessToken; // return unexpired token.

            if (_refresher is null || storedDetails.RefreshToken is null || storedDetails.User.ClientId is null)
                return storedDetails.AccessToken; // return expired token optimistically.

            try
            {
                AccessTokenRefreshResponse refresh = await _refresher.RefreshUserAccessToken(storedDetails.User.ClientId.Value, storedDetails.RefreshToken.Value, ct);
                ImmutableHashSet<Scope> refreshScopes = refresh.Scope?.ToImmutableHashSet() ?? [];
                UserAccessTokenKey refreshedKey = key with { ValidScopes = refreshScopes };
                UserAccessTokenDetails refreshedDetails = storedDetails with
                {
                    AccessToken = refresh.AccessToken,
                    RefreshToken = refresh.RefreshToken,
                    ExpiresAt = DateTimeOffset.UtcNow + refresh.ExpiresIn,
                    Scopes = refreshScopes,
                };
                await _tokenStore.SaveTokenDetails(refreshedKey, refreshedDetails, ct);
                return refreshedDetails.AccessToken; // Return refreshed token.
            }
            catch (TwitchApiException apiException)
            {
                // This could be caused by a number of things (revoked token, rate limits, bad request, etc.)
                return storedDetails.AccessToken; // return expired token optimistically.
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <returns>Always returns <see langword="null"/>.</returns>
    private async ValueTask<UserAccessToken?> RequestNewToken(UserAccessTokenKey key, CancellationToken ct)
    {
        if (_newTokenRequester is not null)
            await _newTokenRequester.RequestUserAccessToken(key, ct);
        return null;
    }
}
