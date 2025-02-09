using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.Integration.Helix.Chat;
[Collection("helix")]
public class Test_GetUserChatColor(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetUserChatColorRequest_ReturnSuccessResponse()
    {
        string userId = await _fixture.GetUserIdFromAccessTokenAsync(); // can be any id

        await _fixture.Api.SendRequestAsync(new GetUserChatColorRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            [userId]
            ));
    }
}
