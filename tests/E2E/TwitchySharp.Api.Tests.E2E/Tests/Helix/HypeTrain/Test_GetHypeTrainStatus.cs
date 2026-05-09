using TwitchySharp.Api.Helix.HypeTrain;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.HypeTrain;

[Collection("twitch")]
public class Test_GetHypeTrainStatus(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetHypeTrainStatusRequest_ReturnSuccessResponse()
    {
        GetHypeTrainStatusRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
