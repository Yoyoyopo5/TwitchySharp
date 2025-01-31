using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization.Extensions;
using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.Integration.Helix.Extensions;
[Collection("helix")]
public class Test_SendExtensionChatMessage(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_SendExtensionChatMessageRequest_ReturnSuccessResponse()
    {
        const string EXTENSION_VERSION = "0.0.1";
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string jwt = new ExtensionJwtPayload(broadcasterId)
            .Sign(_fixture.Secrets.Extension.Secret);

        await _fixture.Api.SendRequestAsync(new SendExtensionChatMessageRequest(
            _fixture.Secrets.Extension.Id,
            jwt,
            broadcasterId,
            new SendExtensionChatMessageRequestData()
            {
                ExtensionId = _fixture.Secrets.Extension.Id,
                ExtensionVersion = EXTENSION_VERSION,
                Text = "test message"
            }
            ));
    }
}
