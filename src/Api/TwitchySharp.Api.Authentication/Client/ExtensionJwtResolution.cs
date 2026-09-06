using System.Text.Json;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Authentication;

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

    // Required configured ExtensionSecret resolver
    public static TwitchClient UseExtensionJwts(
        this TwitchClient client,
        ITwitchTokenCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>? cache = null,
        Func<DateTimeOffset>? getNow = null,
        Func<TwitchIdentity.Extension, DateTimeOffset>? getNewTokenExpiry = null,
        Func<ExtensionJwtPayload, string>? serializePayload = null
        )
    {
        cache ??= new InMemoryConcurrentCache<TwitchIdentity.Extension, AccessTokenDetails.ExtensionJwt>();
        getNow ??= () => DateTimeOffset.UtcNow;
        getNewTokenExpiry ??= _ => DateTimeOffset.UtcNow + TimeSpan.FromMinutes(120);
        serializePayload ??= payload => JsonSerializer.Serialize(payload, JsonConfig.ApiOptions);

        return client.Configure<TwitchClient, BearerToken?>(next => next.WhenTokenTypeIs(
            BearerTokenType.ExtensionJwt,
            SignNewJwt(getNewTokenExpiry, serializePayload)
                .Map(details => details)
                .WithCache(cache, cached => cached.ExpiresAt > getNow())
                .Map(details => details?.BearerToken)
            ));
    }
}
