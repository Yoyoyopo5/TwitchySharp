using TwitchySharp.Api.Helix.Schedule;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Schedule;

public class Test_GetChannelStreamSchedule(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-channel-stream-schedule");

    [Fact]
    public async Task Send_GetChannelStreamScheduleRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetChannelStreamScheduleRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
