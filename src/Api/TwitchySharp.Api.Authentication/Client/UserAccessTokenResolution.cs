using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Authentication;

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
            TwitchResponse<AccessTokenRefreshResponse> response = await twitchClient.SendAsync(request, ct);
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
        ITwitchTokenCache<TwitchIdentity.User, AccessTokenDetails.User> cache
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

    public static TwitchClient UseUserAccessTokens(
        this TwitchClient client,
        ITwitchTokenCache<TwitchIdentity.User, AccessTokenDetails.User> cache,
        Func<DateTimeOffset>? getNow = null
        )
    {
        getNow ??= () => DateTimeOffset.UtcNow;

        return client.Configure<TwitchClient, BearerToken?>(next => next.WhenTokenTypeIs(
                BearerTokenType.UserAccessToken,
                GetFromCache(cache)
                    .RefreshExpired(getNow)
                    .WithCache(cache, cached => cached.ExpiresAt > getNow())
                    .Map(details => details?.BearerToken)
            ));
    }
}
