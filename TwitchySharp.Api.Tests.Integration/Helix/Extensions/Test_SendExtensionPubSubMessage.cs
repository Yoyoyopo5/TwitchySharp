using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_SendExtensionPubSubMessage(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_ExtensionPubSubMessageRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string jwt = new ExtensionJwtPayload(broadcasterId) { ChannelId = broadcasterId }
            .Sign(_fixture.Secrets.Extension.Secret);

        await _fixture.Api.SendRequestAsync(new SendExtensionPubSubMessageRequest(
            _fixture.Secrets.Extension.Id,
            jwt,
            new BroadcastPubSubMessageData()
            {
                Message = "test message"
            }.To(broadcasterId)
            ));
    }
}
