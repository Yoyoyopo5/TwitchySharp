using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Videos;

namespace TwitchySharp.Api.Tests.Integration.Helix.Videos;
[Collection("helix")]
public class Test_GetVideos(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_GetVideosRequest_ReturnSuccessResponse()
    {
        const string gameId = "33214"; // Playin Fortnite
        const string userId = "641972806"; // Kai Cenat

        string videoId = (await GetVideos(new VideoGameQuery(gameId))).Data.First().Id;
        await GetVideos(new VideoIdQuery([videoId]));
        await GetVideos(new VideoUserQuery(userId));
    }

    private ValueTask<GetVideosResponse> GetVideos(GetVideosRequestQueryParameters query)
        => _fixture.Api.SendRequestAsync(new GetVideosRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            query
            ));
}
