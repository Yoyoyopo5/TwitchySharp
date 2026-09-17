using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Authentication;

/// <summary>
/// <see cref="TwitchClient"/> extensions for resolving <see cref="UserAccessToken"/>s for requests.
/// </summary>
public static class UserAccessTokenResolution
{
    private static async ValueTask<Validation<AccessTokenDetails.User>> RefreshToken(
        this ITwitchClient twitchClient,
        ClientId clientId,
        ClientSecret clientSecret,
        UserId userId,
        RefreshToken refreshToken,
        CancellationToken ct
        )
    {
        AccessTokenRefreshRequest request = new()
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
            RefreshToken = refreshToken
        };

        try
        {
            TwitchResponse<AccessTokenRefreshResponseContent> response = await twitchClient.SendAsync(request, ct);
            return response.Content.ToAccessTokenDetails(
                clientId,
                userId,
                DateTimeOffset.Now
                );
        }
        catch (Exception ex)
        {
            return new ExceptionError(ex);
        }
    }

    private static async ValueTask<Validation<AccessTokenDetails.User>> RefreshToken(
        this ITwitchClient client,
        AccessTokenDetails.User expiredDetails,
        ClientSecret clientSecret,
        CancellationToken ct
        )
        => expiredDetails.Identity.ClientId is not ClientId clientId
            ? (Validation<AccessTokenDetails.User>)new Error($"{nameof(AccessTokenDetails.User)} missing required {nameof(ClientId)} when attempting refresh.")
            : expiredDetails.RefreshToken is not RefreshToken refreshToken
            ? (Validation<AccessTokenDetails.User>)new Error($"{nameof(AccessTokenDetails.User)} missing required {nameof(Authentication.RefreshToken)} when attempting refresh.")
            : await client.RefreshToken(clientId, clientSecret, expiredDetails.Identity.UserId, refreshToken, ct);

    private static ResolveRequestDependency<AccessTokenDetails.User?> GetFromCache(
        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache
        )
        => (context, ct) => context.ResolveOrDefault<TwitchIdentity.User>(ct)
            .MapAsync(identity => identity is null
                ? ValueTask.FromResult<AccessTokenDetails.User?>(null)
                : cache.GetOrDefault(identity, ct));

    private static ResolveRequestDependency<AccessTokenDetails.User?> RefreshExpired(
        this ResolveRequestDependency<AccessTokenDetails.User?> next,
        Func<DateTimeOffset> getNow
        )
        => (scope, ct) => next(scope, ct).BindAsync(details => (details is null || details.ExpiresAt > getNow())
                ? ValueTask.FromResult<Validation<AccessTokenDetails.User?>>(details)
                : scope.ResolveRequired<ITwitchClient>(ct)
                    .BindAsync(twitchClient => scope.ResolveRequired<ClientSecret?>(ct)
                    .BindAsync(clientSecret => twitchClient.RefreshToken(details, clientSecret!.Value, ct).MapAsync<AccessTokenDetails.User, AccessTokenDetails.User?>(details => details))));

    /// <summary>
    /// Configure a <see cref="TwitchClient"/> to resolve <see cref="UserAccessToken"/>s
    /// for requests requiring them using a specified token cache.
    /// </summary>
    /// <param name="client">The client to configure.</param>
    /// <param name="cache">The user token cache to use.</param>
    /// <param name="getNow">
    /// A function that returns the time that token expiry should be compared against.
    /// <para>
    /// If <see langword="null"/>, a function returning <see cref="DateTimeOffset.UtcNow"/>.
    /// </para>
    /// </param>
    /// <returns>The configured client.</returns>
    public static TwitchClient UseUserAccessTokens(
        this TwitchClient client,
        IRequestDependencyCache<TwitchIdentity.User, AccessTokenDetails.User> cache,
        Func<DateTimeOffset>? getNow = null
        )
    {
        getNow ??= () => DateTimeOffset.UtcNow;

        return client.WhenTokenTypeIs(BearerTokenType.UserAccessToken)
            .ConfigureAsNullCoalesce(
                GetFromCache(cache)
                    .RefreshExpired(getNow)
                    .WithCache(cache, cached => cached.ExpiresAt > getNow())
                    .Map(details => details?.BearerToken)
            )
            .EndWhen();
    }
}
