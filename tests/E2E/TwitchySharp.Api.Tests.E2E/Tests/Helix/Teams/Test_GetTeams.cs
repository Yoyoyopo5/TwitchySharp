using TwitchySharp.Api.Helix.Teams;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Teams;

[Collection("twitch")]
public class Test_GetTeams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetTeamsRequest_ReturnSuccessResponse()
    {
        GetTeamsRequest request = new()
        {
            Query = new TeamsQueryByName()
            {
                Name = "StreamTeam"
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
