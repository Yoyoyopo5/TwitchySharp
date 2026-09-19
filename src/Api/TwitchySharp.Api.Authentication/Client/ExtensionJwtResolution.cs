using System.Text.Json;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Authentication;

/// <summary>
/// <see cref="TwitchClient"/> extensions for resolving <see cref="ExtensionJsonWebToken"/>s for requests.
/// </summary>
public static class ExtensionJwtResolution
{
    private static ResolveRequestDependency<AccessTokenDetails.ExtensionJwt> SignNewJwt(
        Func<TwitchIdentity.Extension, DateTimeOffset> nextExpiry,
        Func<ExtensionJwtPayload, string> serializePayload
        )
        => (scope, ct) => scope.ResolveRequired<TwitchIdentity.Extension>(ct)
            .BindAsync(extensionIdentity => scope.ResolveRequired<ExtensionSecret?>(ct)
            .BindAsync(extensionSecret => scope.ResolveRequired<ExtensionOwnerId?>(ct)
            .MapAsync(ownerId => new AccessTokenDetails.ExtensionJwt(
                extensionIdentity,
                new ExtensionJwtPayload()
                {
                    UserId = ownerId!.Value,
                    ChannelId = extensionIdentity.BroadcasterId,
                    ExpiresAt = nextExpiry(extensionIdentity)
                }.Sign(extensionSecret!.Value, serializePayload))
                )));

    /// <summary>
    /// Configure a <see cref="TwitchClient"/> to resolve <see cref="ExtensionJsonWebToken"/>s for requests requiring them
    /// using a default token singing and caching strategy.
    /// </summary>
    /// <remarks>
    /// Using this feature requires a configured <see cref="ExtensionSecret"/> and <see cref="ExtensionOwnerId"/> resolver to be configured
    /// in order to sign new <see cref="ExtensionJsonWebToken"/>s.
    /// You can use <see cref="WithExtension"/> to configure an <see cref="ExtensionSecret"/>
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <param name="cache">
    /// The token cache to use.
    /// If a token for the specific <see cref="TwitchIdentity.Extension"/> is not in the cache,
    /// or if it is expired, a new one is signed and added to the cache.
    /// <para>
    /// If <see langword="null"/>, a default in-memory cache is used.
    /// </para>
    /// </param>
    /// <param name="getNewTokenExpiry">
    /// A function that determines when a newly signed token should expire.
    /// <para>
    /// If <see langword="null"/>, a default expiry of 120 minutes from <paramref name="getNow"/> is used.
    /// </para>
    /// </param>
    /// <param name="lockFactory">
    /// <inheritdoc cref="AppAccessTokenResolution.UseAppAccessTokens" path="/param[@name = 'lockFactory']"/>
    /// </param>
    /// <param name="serializePayload">
    /// A function mapping the <see cref="ExtensionJwtPayload"/> to a <see langword="string"/> before signing the JWT.
    /// <para>
    /// If <see langword="null"/>, <see cref="JsonSerializer"/> is used.
    /// </para>
    /// </param>
    /// <param name="getNow"><inheritdoc cref="AppAccessTokenResolution.UseAppAccessTokens" path="/param[@name = 'getNow']"/></param>
    /// <returns>The configured client.</returns>
    public static TwitchClient UseExtensionJwts(
        this TwitchClient client,
        IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>? cache = null,
        Func<TwitchIdentity.Extension, DateTimeOffset>? getNewTokenExpiry = null,
        Func<TwitchIdentity.Extension, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null,
        Func<ExtensionJwtPayload, string>? serializePayload = null,
        Func<DateTimeOffset>? getNow = null
        )
    {
        cache ??= new InMemoryConcurrentCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>();
        getNow ??= () => DateTimeOffset.UtcNow;
        getNewTokenExpiry ??= _ => DateTimeOffset.UtcNow + TimeSpan.FromMinutes(120);
        serializePayload ??= payload => JsonSerializer.Serialize(payload, JsonConfig.ApiOptions);

        return client.WhenTokenTypeIs(BearerTokenType.ExtensionJwt)
            .ConfigureAsNullCoalesce(
                SignNewJwt(getNewTokenExpiry, serializePayload)
                    .Map(details => details)
                    .WithCache(cache, cached => cached.ExpiresAt > getNow())
                    .SerializeBy(lockFactory)
                    .Map(details => details?.BearerToken)
            )
            .EndWhen();
    }

    /// <summary>
    /// Configure a <see cref="TwitchClient"/> to resolve an <see cref="ExtensionOwnerId"/> and
    /// <see cref="ExtensionSecret"/> for a specific <see cref="ExtensionId"/>.
    /// </summary>
    /// <remarks>
    /// This can be used with <see cref="UseExtensionJwts"/> to configure a default flow
    /// for signing new JWTs for requests requiring them.
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <param name="extensionId">The id of the extension to configure an <see cref="ExtensionSecret"/> and <see cref="ExtensionOwnerId"/> for.</param>
    /// <param name="ownerId">The user id of the owner (creator) of the extension.</param>
    /// <param name="secret">
    /// One of the extension's shared secrets.
    /// These can be found on the <see href="https://dev.twitch.tv/console/extensions">Twitch Developer Console</see> under the extension configuration,
    /// or they can be managed programatically via the Helix <see href="https://dev.twitch.tv/docs/api/reference/#create-extension-secret">Create Extension Secret</see> and
    /// <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-secrets">Get Extension Secrets</see> endpoints.
    /// </param>
    /// <returns>The configured client.</returns>
    public static TwitchClient WithExtension(
        this TwitchClient client,
        ExtensionId extensionId,
        ExtensionOwnerId ownerId,
        ExtensionSecret secret
        )
        => client
            .When((scope, ct) => scope.ResolveOrDefault<ExtensionId?>(ct).MapAsync(id => id == extensionId))
            .SetFixed((ExtensionOwnerId?)ownerId)
            .SetFixed((ExtensionSecret?)secret)
            .EndWhen();
}
