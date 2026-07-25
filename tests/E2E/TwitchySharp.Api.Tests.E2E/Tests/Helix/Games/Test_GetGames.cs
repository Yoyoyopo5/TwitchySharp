using TwitchySharp.Api.Helix.Games;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Games;

public class Test_GetGames(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-games");

    [Fact]
    public async Task Send_GetGamesRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string GAME_NAME = "Fortnite";
        const string GAME_ID = "2627";
        GameId gameId = new(GAME_ID);
        const string IGDB_ID = "125633";
        IgdbId igdbId = new(IGDB_ID);

        GetGamesRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            Games = [new GameNameQuery(GAME_NAME), new GameIdQuery(gameId), new GameIgdbQuery(igdbId)]
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
