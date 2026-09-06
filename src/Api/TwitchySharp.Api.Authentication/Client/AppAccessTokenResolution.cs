using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Authentication;

public static class AppAccessTokenResolution
{
    private static async ValueTask<Validation<AccessTokenDetails.App>> GetNewAppAccessToken(
        this ITwitchClient twitchClient,
        ClientId clientId,
        ClientSecret clientSecret,
        DateTimeOffset now,
        CancellationToken ct
        )
    {
        try
        {
            TwitchResponse<ClientCredentialsResponseContent> credentialsResponse =
                await twitchClient.SendAsync(new ClientCredentialsRequest()
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                }, ct);
            return credentialsResponse.Content.ToAccessTokenDetails(clientId, now);
        }
        catch (Exception ex)
        {
            return new ExceptionError(ex);
        }
    }

    private static ResolveRequestDependency<AccessTokenDetails.App> GetTokenFromTwitch(
        Func<DateTimeOffset> getNow
        )
        => (scope, ct) => scope.ResolveRequired<ClientId?>(ct)
            .BindAsync(clientId => scope.ResolveRequired<ClientSecret?>(ct)
            .BindAsync(clientSecret => scope.ResolveRequired<ITwitchClient>(ct)
            .BindAsync(twitchClient => twitchClient.GetNewAppAccessToken(clientId!.Value, clientSecret!.Value, getNow(), ct))));

    // Requires configured ClientSecret resolver
    public static TwitchClient UseAppAccessTokens(
        this TwitchClient client,
        ITwitchTokenCache<ClientId, AccessTokenDetails.App>? tokenCache = null,
        Func<DateTimeOffset>? getNow = null
        )
    {
        tokenCache ??= new InMemoryConcurrentCache<ClientId, AccessTokenDetails.App>();
        getNow ??= () => DateTimeOffset.UtcNow;

        return client.Configure<TwitchClient, BearerToken?>(next => next.WhenTokenTypeIs(
                BearerTokenType.AppAccessToken,
                GetTokenFromTwitch(getNow)
                    .Map(details => details) // map to nullable
                    .WithCache(tokenCache, details => details.ExpiresAt > getNow())
                    .Map(details => details?.BearerToken)));
    }
}
