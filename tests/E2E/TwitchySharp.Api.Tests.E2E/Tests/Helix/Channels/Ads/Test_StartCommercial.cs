using TwitchySharp.Api.Helix.Ads;
using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels.Ads;

public class Test_StartCommercial(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("start-commercial");

    [Fact]
    public async Task Send_StartCommercialRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SkipIfBroadcasterIsNotStreaming(userConfig.UserId, ct);

        GetAdScheduleRequest getAdScheduleRequest = new()
        {
            BroadcasterId = userConfig.UserId
        };

        TwitchResponse<GetAdScheduleResponseContent> getAdScheduleResponse = await client.SendAsync(getAdScheduleRequest, TestName, ct);
        AdSchedule? schedule = getAdScheduleResponse.Content.Data.SingleOrDefault();

        // This may not be required to test StartCommercial
        //Assert.SkipWhen(
        //    schedule is null,
        //    "The broadcaster does not have an ad schedule."
        //    );

        // Min 8 minutes between ads
        Assert.SkipWhen(
            schedule is not null && schedule.LastAdAt > DateTimeOffset.UtcNow - TimeSpan.FromMinutes(8),
            "The broadcaster's last ad was run too shortly ago."
            );

        StartCommercialRequest request = new()
        {
            Commercial = new()
            {
                BroadcasterId = userConfig.UserId,
                Length = TimeSpan.FromSeconds(30)
            }
        };

        await client.SendAsync(request, TestName, ct);
    }
}
