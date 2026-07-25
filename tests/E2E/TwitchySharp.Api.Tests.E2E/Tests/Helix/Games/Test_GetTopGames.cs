using TwitchySharp.Api.Helix.Games;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Games;

public class Test_GetTopGames(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-top-games");

    [Fact]
    public async Task Send_GetTopGamesRequest_ReturnSuccessResponse()
    {
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        GetTopGamesRequest request = new()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
