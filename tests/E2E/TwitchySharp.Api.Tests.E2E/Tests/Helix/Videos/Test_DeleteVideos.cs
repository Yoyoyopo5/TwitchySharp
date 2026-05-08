using TwitchySharp.Api.Helix.Videos;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Videos;

[Collection("twitch")]
public class Test_DeleteVideos(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_DeleteVideosRequest_ReturnSuccessResponse()
    {
        // Note this has side effect of deleting most recent video on test channel. oof
        UserId broadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetVideosRequest getRequest = new()
        {
            Query = new VideoUserQuery()
            {
                UserId = broadcasterId
            }
        };

        var getResponse = await client.SendAsync(getRequest, ct);
        if (getResponse.Content.Data.FirstOrDefault() is not TwitchVideo video)
            return;

        DeleteVideosRequest deleteRequest = new()
        {
            UserId = broadcasterId,
            Ids = [video.Id]
        };

        await client.SendAsync(deleteRequest, ct);
    }
}
