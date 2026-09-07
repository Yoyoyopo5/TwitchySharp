using TwitchySharp.Api.Helix.Teams;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Teams;

public class Test_GetChannelTeams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-channel-teams");

    [Fact]
    public async Task Send_GetChannelTeamsRequest_ReturnSuccessResponse()
    {
        // I don't have access to an account in a team, so not 100% sure this deserializes correctly.
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        // We may want to use app token for this.
        GetChannelTeamsRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
