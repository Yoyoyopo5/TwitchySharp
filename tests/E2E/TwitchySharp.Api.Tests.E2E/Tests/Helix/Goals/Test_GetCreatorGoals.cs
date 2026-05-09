using TwitchySharp.Api.Helix.Goals;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Goals;

[Collection("twitch")]
public class Test_GetCreatorGoals(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetCreatorGoalsRequest_ReturnSuccessResponse()
    {
        GetCreatorGoalsRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
