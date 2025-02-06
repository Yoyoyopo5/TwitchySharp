using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_WarnChatUser(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_WarnChatUserRequest_ReturnSuccessResponse()
    {
        string userToWarn = "52137752";
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new WarnChatUserRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            new WarnChatUserRequestData()
            {
                Data = new()
                {
                    UserId = userToWarn,
                    Reason = "You have been selected for a random test of the warning system."
                }
            }
            ));
    }
}
