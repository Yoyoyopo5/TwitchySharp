using TwitchySharp.Api.Helix.Schedule;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Schedule;

[Collection("twitch")]
public class Test_UpdateChannelStreamSchedule(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateChannelStreamScheduleRequest_ReturnSuccessResponse()
    {
        UpdateChannelStreamScheduleRequest request = new()
        {
            Settings = new UpdateChannelStreamScheduleRequestParameters()
            {
                BroadcasterId = _fixture.UserIdentity.UserId,
            }.EnableVacationMode(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow + TimeSpan.FromDays(1), TimeZoneInfo.Local)
        };

        UpdateChannelStreamScheduleRequest restoreRequest = new()
        {
            Settings = new UpdateChannelStreamScheduleRequestParameters()
            {
                BroadcasterId = _fixture.UserIdentity.UserId,
            }.DisableVacationMode()
        };

        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SendAsync(request, ct);
        await Task.Delay(250, ct);
        await client.SendAsync(restoreRequest, ct);
    }
}
