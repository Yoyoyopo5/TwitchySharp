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
    /// <param name="configureOptions">A function that configures extension JWT options.</param>
    /// <returns>The configured client.</returns>
    public static TwitchClient UseExtensionJwts(
        this TwitchClient client,
        Func<Options, Options>? configureOptions = null
        )
    {
        Options opts = configureOptions is null ? new() : configureOptions(new());

        return client.WhenTokenTypeIs(BearerTokenType.ExtensionJwt)
            .SetResolver(
                SignNewJwt(opts.GetNewTokenExpiry, opts.SerializePayload)
                    .Map(details => details)
                    .WithCache(opts.JwtCache, cached => cached.ExpiresAt > opts.GetNow())
                    .SerializeBy(opts.LockFactory)
                    .Map(details => details?.BearerToken)
            )
            .EndWhen();
    }

    /// <summary>
    /// Contains optional configuration for <see cref="UseExtensionJwts"/>
    /// </summary>
    public record Options
    {
        /// <summary>
        /// The JWT cache to use.
        /// </summary>
        /// <remarks>
        /// If a JWT for the specific <see cref="TwitchIdentity.Extension"/> is not in the cache,
        /// or if it is expired, a new one is signed and added to the cache.
        /// <para>
        /// By default, an in-memory cache is used.
        /// </para>
        /// </remarks>
        public IRequestDependencyCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt> JwtCache { get; init; }
            = new InMemoryConcurrentCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>();

        /// <summary>
        /// A function that determines when a newly signed token should expire.
        /// </summary>
        /// <remarks>
        /// By default, an expiry of 120 minutes from <see cref="DateTimeOffset.UtcNow"/> is used.
        /// </remarks>
        public Func<TwitchIdentity.Extension, DateTimeOffset> GetNewTokenExpiry { get; init; }
            = _ => DateTimeOffset.UtcNow + TimeSpan.FromMinutes(120);

        /// <inheritdoc cref="AppAccessTokenResolution.Options.LockFactory"/>
        public Func<TwitchIdentity.Extension, CancellationToken, ValueTask<IAsyncDisposable>>? LockFactory { get; init; }

        /// <summary>
        /// A function mapping the <see cref="ExtensionJwtPayload"/> to a <see langword="string"/> before signing the JWT.
        /// </summary>
        /// <remarks>
        /// By default, <see cref="JsonSerializer"/> is used.
        /// </remarks>
        public Func<ExtensionJwtPayload, string> SerializePayload { get; init; }
            = payload => JsonSerializer.Serialize(payload, JsonConfig.ApiOptions);

        /// <inheritdoc cref="AppAccessTokenResolution.Options.GetNow"/>
        public Func<DateTimeOffset> GetNow { get; init; }
            = () => DateTimeOffset.UtcNow - TimeSpan.FromSeconds(1);
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
