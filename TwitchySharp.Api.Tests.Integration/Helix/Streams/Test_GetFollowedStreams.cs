using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Streams;

namespace TwitchySharp.Api.Tests.Integration.Helix.Streams;
[Collection("helix")]
public class Test_GetFollowedStreams(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetFollowedStreamsRequest_ReturnSuccessResponse()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new GetFollowedStreamsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId
            ));
    }
}
