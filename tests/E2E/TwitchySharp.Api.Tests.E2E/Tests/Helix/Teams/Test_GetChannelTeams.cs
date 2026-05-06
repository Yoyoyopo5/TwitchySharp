using TwitchySharp.Api.Helix.Teams;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Teams;

[Collection("twitch")]
public class Test_GetChannelTeams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetChannelTeamsRequest_ReturnSuccessResponse()
    {
        // I don't have access to an account in a team, so not 100% sure this deserializes correctly.
        GetChannelTeamsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
