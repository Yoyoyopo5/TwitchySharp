using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Authentication;

/// <summary>
/// <see cref="TwitchClient"/> extensions for resolving <see cref="AppAccessToken"/>s for requests.
/// </summary>
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

    internal static RequestDependencyConditionalConfiguration<TwitchClient> WhenTokenTypeIs(
        this TwitchClient client,
        BearerTokenType tokenType
        )
        => client.When((scope, ct) => scope.ResolveOrDefault<BearerTokenType?>(ct).MapAsync(type => type == tokenType));

    /// <summary>
    /// Configure a <see cref="TwitchClient"/> to resolve <see cref="AppAccessToken"/>s for requests requiring them
    /// using a default token acquisition flow and caching strategy.
    /// </summary>
    /// <remarks>
    /// Using this feature requires a <see cref="ClientSecret"/> resolver to be configured
    /// in order to acquire new <see cref="AppAccessToken"/>s from Twitch.
    /// You can use <see cref="WithClient"/> to configure a mapping between a <see cref="ClientId"/>
    /// and a <see cref="ClientSecret"/>, or configure a <see cref="ClientSecret"/> resolver manually.
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <param name="tokenCache">
    /// The app access token cache to use.
    /// Tokens are preferentially pulled from this cache.
    /// If the requested token is not in the cache, or if it is expired,
    /// a new token will be acquired from Twitch using <see cref="ClientCredentialsRequest"/>
    /// and stored in the cache.
    /// <para>
    /// If <see langword="null"/>, a default in-memory cache is used.
    /// </para>
    /// </param>
    /// <param name="lockFactory">
    /// <para>
    /// By default, checking the cache, acquiring a new token, and updating the cache are performed
    /// in serial with other requests using the same identity to avoid two parallel requests
    /// from both acquiring new tokens.
    /// </para>
    /// <para>
    /// Each request waits for an <see cref="IAsyncDisposable"/> from this function before performing cache operations,
    /// disposing it once the access token is resolved.
    /// </para>
    /// <para>
    /// If <see langword="null"/>, a default in-memory lock provider is used.
    /// </para>
    /// </param>
    /// <param name="getNow">
    /// A function that returns the current time.
    /// This is used for evaluating token expiry.
    /// <para>
    /// If <see langword="null"/>, a function returning <see cref="DateTimeOffset.UtcNow"/> is used.
    /// </para>
    /// </param>
    /// <returns>The configured client.</returns>
    public static TwitchClient UseAppAccessTokens(
        this TwitchClient client,
        IRequestDependencyCache<ClientId, AccessTokenDetails.App>? tokenCache = null,
        Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null,
        Func<DateTimeOffset>? getNow = null
        )
    {
        tokenCache ??= new InMemoryConcurrentCache<ClientId, AccessTokenDetails.App>();
        getNow ??= () => DateTimeOffset.UtcNow;

        return client.WhenTokenTypeIs(BearerTokenType.AppAccessToken)
            .ConfigureAsNullCoalesce(
                GetTokenFromTwitch(getNow)
                    .Map(details => details) // map to nullable
                    .WithCache(tokenCache, details => details.ExpiresAt > getNow())
                    .SerializeByValue(lockFactory)
                    .Map(details => details?.BearerToken))
            .EndWhen();
    }

    /// <summary>
    /// Configure a <see cref="TwitchClient"/> to resolve a <see cref="ClientSecret"/> for a specific <see cref="ClientId"/>.
    /// </summary>
    /// <remarks>
    /// This can be used in conjunction with <see cref="UseAppAccessTokens"/> to configure a
    /// default flow for acquiring and caching app access tokens.
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <param name="clientId">The <see cref="ClientId"/> to configure a <see cref="ClientSecret"/> for.</param>
    /// <param name="clientSecret">The <see cref="ClientSecret"/> that the <paramref name="clientId"/> should use.</param>
    /// <returns>The configured client.</returns>
    public static TwitchClient WithClient(
        this TwitchClient client,
        ClientId clientId,
        ClientSecret clientSecret
        )
        => client
            .When((scope, ct) => scope.ResolveOrDefault<ClientId?>(ct).MapAsync(id => id == clientId))
            .SetFixed((ClientSecret?)clientSecret)
            .EndWhen();
}
