using TwitchySharp.Api.Helix.Schedule;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Schedule;

[Collection("twitch")]
public class Test_GetChannelStreamSchedule(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetChannelStreamScheduleRequest_ReturnSuccessResponse()
    {
        GetChannelStreamScheduleRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
