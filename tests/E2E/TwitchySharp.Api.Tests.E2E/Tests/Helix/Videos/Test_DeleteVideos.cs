using TwitchySharp.Api.Helix.Videos;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Videos;

public class Test_DeleteVideos(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("delete-videos");

    [Fact]
    public async Task Send_DeleteVideosRequest_ReturnSuccessResponse()
    {
        // Note this has side effect of deleting most recent video on test channel. oof
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UserId broadcasterId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetVideosRequest getRequest = new()
        {
            Query = new VideoUserQuery()
            {
                UserId = broadcasterId
            }
        };

        TwitchResponse<GetVideosResponseContent> getResponse = await client.SendAsync(getRequest, TestName, ct);
        if (getResponse.Content.Data.FirstOrDefault() is not TwitchVideo video)
            return;

        DeleteVideosRequest deleteRequest = new()
        {
            UserId = broadcasterId,
            Ids = [video.Id]
        };

        await client.SendAsync(deleteRequest, TestName, ct);
    }
}
