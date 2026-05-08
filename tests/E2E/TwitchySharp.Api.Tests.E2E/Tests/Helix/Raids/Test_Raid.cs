using TwitchySharp.Api.Helix.Raids;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Raids;

[Collection("twitch")]
public class Test_Raid(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_RaidRequests_ReturnSuccessResponses()
    {
        const string TO_BROADCASTER_ID = "141879576"; // dreadbreadcrumb
        UserId toBroadcasterId = new(TO_BROADCASTER_ID);
        UserId fromBroadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = _fixture.CreateClient();
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

        await client.SendAsync(startRaidRequest, ct);
        await Task.Delay(250, ct);
        await client.SendAsync(cancelRaidRequest, ct);
    }
}
