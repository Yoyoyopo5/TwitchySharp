using TwitchySharp.Api.Helix.Ads;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels.Ads;

public class Test_SnoozeNextAd(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("snooze-next-ad");

    [Fact]
    public async Task Send_SnoozeNextAdRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetAdScheduleRequest adScheduleRequest = new()
        {
            BroadcasterId = userConfig.UserId
        };

        TwitchResponse<GetAdScheduleResponse> adScheduleResponse = await client.SendAsync(adScheduleRequest, ct);
        AdSchedule? schedule = adScheduleResponse.Content.Data.SingleOrDefault();

        Assert.SkipWhen(
            schedule is null,
            "The broadcaster does not have an ad schedule."
            );

        Assert.SkipWhen(
            schedule.SnoozeCount == 0,
            "The broadcaster does not have any ad snoozes remaining."
            );

        Assert.SkipWhen(
            schedule.NextAdAt == DateTimeOffset.MinValue,
            "The broadcaster has no upcoming ad scheduled."
            );

        SnoozeNextAdRequest request = new()
        {
            BroadcasterId = userConfig.UserId
        };

        await client.SendAsync(request, ct);
    }
}
