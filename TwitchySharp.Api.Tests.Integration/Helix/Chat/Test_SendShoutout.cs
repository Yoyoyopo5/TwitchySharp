using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.Integration.Helix.Chat;
[Collection("helix")]
public class Test_SendShoutout(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_SendShoutoutRequest_ReturnSuccessResponse()
    {
        const string TO_BROADCASTER_ID = "52137752";
        string fromBroadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new SendShoutoutRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            fromBroadcasterId,
            TO_BROADCASTER_ID,
            fromBroadcasterId
            ));
    }
}
