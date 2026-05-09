using TwitchySharp.Api.Helix.Games;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Games;

[Collection("twitch")]
public class Test_GetGames(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetGamesRequest_ReturnSuccessResponse()
    {
        const string GAME_NAME = "Fortnite";
        const string GAME_ID = "2627";
        GameId gameId = new(GAME_ID);
        const string IGDB_ID = "125633";
        IgdbId igdbId = new(IGDB_ID);

        GetGamesRequest request = new()
        {
            Games = [new GameNameQuery(GAME_NAME), new GameIdQuery(gameId), new GameIgdbQuery(igdbId)]
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
