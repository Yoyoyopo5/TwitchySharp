using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Api.Helix.Videos;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

public class Test_GetStreamMarkers(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-stream-markers");

    [Fact]
    public async Task Send_GetStreamMarkersRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetVideosRequest getVideosRequest = new() { Query = new VideoUserQuery() { UserId = userConfig.UserId } };
        TwitchResponse<GetVideosResponseContent> getVideosResponse = await client.SendAsync(getVideosRequest, TestName, ct);

        Assert.SkipWhen(
            getVideosResponse.Content.Data.Length == 0,
            "Broadcaster has no videos :("
            );

        GetStreamMarkersRequest request = new()
        {
            Query = new BroadcasterStreamMarkersQuery()
            {
                UserId = userConfig.UserId
            },
            UserId = userConfig.UserId,
        };

        await client.SendAsync(request, TestName, ct);
    }
}
