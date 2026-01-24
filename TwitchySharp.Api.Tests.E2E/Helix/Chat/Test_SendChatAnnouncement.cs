using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat.Enums;
using TwitchySharp.Api.Models.Helix.Chat.Requests;

namespace TwitchySharp.Api.Tests.E2E.Helix.Chat;
[Collection("helix")]
public class Test_SendChatAnnouncement(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_SendChatAnnouncementRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new SendChatAnnouncementRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            new SendChatAnnouncementRequestData()
            {
                Color = ChatAnnouncementColor.Primary,
                Message = "test announcement"
            }
            ));
    }
}
