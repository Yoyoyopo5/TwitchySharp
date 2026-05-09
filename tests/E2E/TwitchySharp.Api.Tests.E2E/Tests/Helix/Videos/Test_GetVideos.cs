using TwitchySharp.Api.Helix.Videos;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Videos;

[Collection("twitch")]
public class Test_GetVideos(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetVideosRequest_ReturnSuccessResponse()
    {
        const string TEST_GAME_ID = "33214"; // Playin Fortnite
        const string TEST_USER_ID = "641972806"; // Kai Cenat

        GameId gameId = new(TEST_GAME_ID);
        UserId userId = new(TEST_USER_ID);

        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetVideosRequest byGame = new()
        {
            Query = new VideoGameQuery()
            {
                GameId = gameId
            }
        };

        var byGameResponse = await client.SendAsync(byGame, ct);
        if (byGameResponse.Content.Data.FirstOrDefault() is not TwitchVideo video)
            return;

        GetVideosRequest byUser = new()
        {
            Query = new VideoUserQuery()
            {
                UserId = userId
            }
        };

        await client.SendAsync(byUser, ct);

        GetVideosRequest byId = new()
        {
            Query = new VideoIdQuery()
            {
                Ids = [video.Id]
            }
        };

        await client.SendAsync(byId, ct);
    }
}
