using TwitchySharp.Api.Helix.Bits;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Bits;

public class Test_GetBitsLeaderboardRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private readonly static TestName TestName = new("get-bits-leaderboard");

    [Fact]
    public async Task Send_GetBitsLeaderboardRequest_ReturnSuccessResponse()
    {
        UserConfiguration? userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetBitsLeaderboardRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
