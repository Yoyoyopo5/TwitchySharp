using TwitchySharp.Api.Helix.Raids;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Raids;

public class Test_Raid(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("raid");

    [Fact]
    public async Task Send_RaidRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TO_BROADCASTER_ID = "56648155";
        UserId toBroadcasterId = new(TO_BROADCASTER_ID);
        UserId fromBroadcasterId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        StartRaidRequest startRaidRequest = new()
        {
            FromBroadcasterId = fromBroadcasterId,
            ToBroadcasterId = toBroadcasterId
        };

        CancelRaidRequest cancelRaidRequest = new()
        {
            BroadcasterId = fromBroadcasterId,
        };

        TwitchResponse<StartRaidResponseContent> startResponse = await client.SendAsync(startRaidRequest, TestName, ct);
        Assert.Null(startResponse.Error);

        await Task.Delay(250, ct);
        TwitchResponse<CancelRaidResponseContent> cancelResponse = await client.SendAsync(cancelRaidRequest, TestName, ct);
        Assert.Null(cancelResponse.Error);
    }
}
