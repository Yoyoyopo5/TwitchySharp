using TwitchySharp.Api;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Tests.E2E;

public static class ExtensionJwtExtensions
{
    public static ValueTask<AccessTokenDetails.ExtensionJwt> SignNewJwt(
        this TwitchIdentity.Extension identity,
        ExtensionSecret extensionSecret
        )
        => ValueTask.FromResult(new AccessTokenDetails.ExtensionJwt()
        {
            Identity = identity,
            AccessToken = new ExtensionJwtPayload()
            {
                UserId = identity.OwnerId,
                ChannelId = identity.BroadcasterId
            }.Sign(new(extensionSecret))
        });
}
