using TwitchySharp.Api.Helix.Videos;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Videos;

public class Test_GetVideos(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-videos");

    [Fact]
    public async Task Send_GetVideosRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        const string TEST_GAME_ID = "33214"; // Playin Fortnite
        const string TEST_USER_ID = "641972806"; // Kai Cenat

        GameId gameId = new(TEST_GAME_ID);
        UserId userId = new(TEST_USER_ID);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetVideosRequest byGame = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            Query = new VideoGameQuery()
            {
                GameId = gameId
            }
        };

        TwitchResponse<GetVideosResponse> byGameResponse = await client.SendAsync(byGame, ct);
        if (byGameResponse.Content.Data.FirstOrDefault() is not TwitchVideo video)
            return;

        GetVideosRequest byUser = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            Query = new VideoUserQuery()
            {
                UserId = userId
            }
        };

        await client.SendAsync(byUser, ct);

        GetVideosRequest byId = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            Query = new VideoIdQuery()
            {
                Ids = [video.Id]
            }
        };

        await client.SendAsync(byId, ct);
    }
}
