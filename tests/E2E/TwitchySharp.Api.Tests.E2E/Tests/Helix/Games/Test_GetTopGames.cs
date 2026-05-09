using TwitchySharp.Api.Helix.Games;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Games;

[Collection("twitch")]
public class Test_GetTopGames(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetTopGamesRequest_ReturnSuccessResponse()
    {
        GetTopGamesRequest request = new();

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
