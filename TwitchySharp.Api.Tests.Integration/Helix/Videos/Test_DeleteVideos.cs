using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Videos;

namespace TwitchySharp.Api.Tests.Integration.Helix.Videos;
[Collection("helix")]
public class Test_DeleteVideos(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_DeleteVideosRequest_ReturnSuccessResponse()
    {
        // Note this has side effect of deleting most recent video on test channel.
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string videoId = (await _fixture.Api.SendRequestAsync(new GetVideosRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new VideoUserQuery(broadcasterId)
            ))).Data.First().Id;

        await _fixture.Api.SendRequestAsync(new DeleteVideosRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            [ videoId ]
            ));
    }
}
