using TwitchySharp.Api.Helix.Schedule;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Schedule;

public class Test_UpdateChannelStreamSchedule(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("update-channel-stream-schedule");

    [Fact]
    public async Task Send_UpdateChannelStreamScheduleRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UpdateChannelStreamScheduleRequest request = new()
        {
            Settings = new UpdateChannelStreamScheduleRequestParameters()
            {
                BroadcasterId = userConfig.UserId,
            }.EnableVacationMode(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow + TimeSpan.FromDays(1), TimeZoneInfo.Local)
        };

        UpdateChannelStreamScheduleRequest restoreRequest = new()
        {
            Settings = new UpdateChannelStreamScheduleRequestParameters()
            {
                BroadcasterId = userConfig.UserId,
            }.DisableVacationMode()
        };

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        TwitchResponse<UpdateChannelStreamScheduleResponseContent> response = await client.SendAsync(request, TestName, ct);
        Assert.Null(response.Error);
        await Task.Delay(250, ct);
        TwitchResponse<UpdateChannelStreamScheduleResponseContent> restoreResponse = await client.SendAsync(restoreRequest, TestName, ct);
        Assert.Null(restoreResponse.Error);
    }
}
