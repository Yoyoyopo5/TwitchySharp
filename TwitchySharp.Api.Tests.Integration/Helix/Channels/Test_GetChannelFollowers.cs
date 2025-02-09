using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Tests.Integration.Helix.Channels;
[Collection("helix")]
public class Test_GetChannelFollowers(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetChannelFollowersRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetChannelFollowersRequest(_fixture.Secrets.Client.Id, _fixture.Secrets.User.AccessToken, broadcasterId));
    }
}
