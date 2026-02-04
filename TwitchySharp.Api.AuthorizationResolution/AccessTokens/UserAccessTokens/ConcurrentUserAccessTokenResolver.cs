using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// A thread-safe implementation of <see cref="IResolveUserAccessToken"/> that retrieves user access tokens from a supplied store using <see cref="UserAccessTokenKey"/>,
/// with support for refreshing expired tokens via a supplied <see cref="IRefreshUserAccessToken"/>.
/// </summary>
/// <remarks>
/// Contains internal state to ensure that concurrent requests for the same user's access token are properly synchronized,
/// register or use as a singleton to avoid race conditions causing multiple token refreshes.
/// </remarks>
/// <param name="tokenStore">The token store to retrieve and store user access tokens with.</param>
/// <param name="refresher">
/// Specify this parameter to allow for proactive refreshing of expired or near-expired tokens.
/// Initialize with the <see cref="ExpirationBuffer"/> to customize the buffer time for considering a token "near-expired".
/// </param>
/// <param name="logger">Optional logger.</param>
public class ConcurrentUserAccessTokenResolver(
    IStoreUserAccessTokens tokenStore,
    IRefreshUserAccessToken? refresher = null,
    ILogger<ConcurrentUserAccessTokenResolver>? logger = null
    )
    : IResolveUserAccessToken
{
    private readonly IStoreUserAccessTokens _tokenStore = tokenStore;
    private readonly IRefreshUserAccessToken? _refresher = refresher;
    private readonly ILogger<ConcurrentUserAccessTokenResolver> _logger = logger ?? NullLogger<ConcurrentUserAccessTokenResolver>.Instance;
    private readonly ConcurrentDictionary<UserIdentity, SemaphoreSlim> _semaphores = new();

    /// <summary>
    /// The amount of time before <see cref="AccessTokenDetails.ExpiresAt"/> to trigger a refresh call.
    /// </summary>
    /// <remarks>
    /// Defaults to 5 minutes.
    /// </remarks>
    public TimeSpan ExpirationBuffer { get; init; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// <list type="bullet">
    /// <item>
    /// <see cref="AccessTokenResolutionResult.Unavailable"/> if the requested <see cref="UserAccessToken"/> does not exist.
    /// </item>
    /// <item>
    /// <see cref="AccessTokenResolutionResult.Valid{TToken}"/> if the requested <see cref="UserAccessToken"/> is unexpired or was expired and successfully refreshed.
    /// </item>
    /// <item>
    /// <see cref="AccessTokenResolutionResult.Expired{TToken}"/> if the requested <see cref="UserAccessToken"/> is expired and could not be refreshed.
    /// </item>
    /// </list>
    /// </returns>
    public async ValueTask<AccessTokenResolutionResult> GetToken(UserAccessTokenKey key, CancellationToken ct = default)
    {
        using IDisposable? tokenResolutionLoggerScope = _logger.BeginScope("Resolving user access token including one of {Scopes} for {UserId} on {ClientId}", key.ValidScopes, key.User.UserId, key.User.ClientId);

        SemaphoreSlim semaphore = _semaphores.GetOrAdd(key.User, _ => new SemaphoreSlim(1, 1)); // Memory leak here, but acceptable for most use cases (each SemaphoreSlim is ~100 bytes).
        await semaphore.WaitAsync(ct);

        _logger.LogTrace("Acquired key lock.");

        try
        {
            if (await TryGetStoredToken(key, ct) is not UserAccessTokenDetails storedDetails)
                return AccessTokenResolutionResult.Unavailable.Instance;

            if (IsTokenValid(storedDetails))
                return new AccessTokenResolutionResult.Valid<UserAccessToken>(storedDetails.AccessToken);

            if (!CanRefreshToken(storedDetails))
                return new AccessTokenResolutionResult.Expired<UserAccessToken>(storedDetails.AccessToken);

            return await RefreshTokenAsync(key, storedDetails, ct);
        }
        finally
        {
            semaphore.Release();
            _logger.LogTrace("Released key lock.");
        }
    }

    private async ValueTask<UserAccessTokenDetails?> TryGetStoredToken(UserAccessTokenKey key, CancellationToken ct)
    {
        if (await _tokenStore.GetTokenDetails(key, ct) is not UserAccessTokenDetails storedDetails)
        {
            _logger.LogDebug("Token details not found in store.");
            return null;
        }

        return storedDetails;
    }

    private bool IsTokenValid(UserAccessTokenDetails details)
        => details.ExpiresAt > DateTimeOffset.UtcNow + ExpirationBuffer;

    private bool CanRefreshToken(UserAccessTokenDetails details)
    {
        if (_refresher is null || details.RefreshToken is null || details.Identity.ClientId is null)
        {
            _logger.LogWarning("Expired token unable to be refreshed due to {MissingDependency}",
                _refresher == null ? nameof(IRefreshUserAccessToken)
                : !details.RefreshToken.HasValue ? nameof(RefreshToken)
                : !details.Identity.ClientId.HasValue ? nameof(ClientId)
                : "missing dependency");
            return false;
        }

        return true;
    }

    private async ValueTask<AccessTokenResolutionResult> RefreshTokenAsync(
        UserAccessTokenKey key,
        UserAccessTokenDetails storedDetails,
        CancellationToken ct)
    {
        _logger.LogDebug("Refreshing user access token.");
        try
        {
            AccessTokenRefreshResponse refresh = await _refresher!.RefreshUserAccessToken(storedDetails.Identity.ClientId!.Value, storedDetails.RefreshToken!.Value, ct);
            _logger.LogDebug("User access token refreshed successfully.");
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
            _logger.LogInformation("Refreshed token details with {ExpiresAt} saved to store.", refreshedDetails.ExpiresAt);
            return new AccessTokenResolutionResult.Valid<UserAccessToken>(refreshedDetails.AccessToken);
        }
        catch (TwitchApiException apiException)
        {
            // This could be caused by a number of things (revoked token, rate limits, bad request, etc.), so we can't necessarily delete the stored token or request a new one.
            _logger.LogWarning(apiException, "Refresh failed with HTTP status code {StatusCode}. Falling back to expired token.", apiException.StatusCode);
            return new AccessTokenResolutionResult.Expired<UserAccessToken>(storedDetails.AccessToken);
        }
    }
}
