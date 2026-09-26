using TwitchySharp.Api.Helix.Teams;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Teams;

public class Test_GetTeams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-teams");

    [Fact]
    public async Task Send_GetTeamsRequest_ReturnSuccessResponse()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        GetTeamsRequest request = new()
        {
            Query = new TeamsQueryByName()
            {
                Name = "StreamTeam"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
