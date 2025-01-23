using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.Integration.Helix.Chat;
[Collection("helix")]
public class Test_UpdateChatSettings(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_UpdateChatSettingsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new UpdateChatSettingsRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            broadcasterId,
            broadcasterId,
            new UpdateChatSettingsRequestData()
            {
                EmoteMode = true
            }
            ));

        await _fixture.Api.SendRequestAsync(new UpdateChatSettingsRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.UserAccessToken,
            broadcasterId,
            broadcasterId,
            new UpdateChatSettingsRequestData()
            {
                EmoteMode = false
            }
            ));
    }
}
