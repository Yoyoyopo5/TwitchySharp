using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_ShieldMode(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateShieldModeStatusRequest_ReturnSuccessResponses()
    {
        UserId broadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await UpdateShieldModeStatus(client, broadcasterId, true, ct);
        await Task.Delay(250, ct);
        await UpdateShieldModeStatus(client, broadcasterId, false, ct);
    }

    [Fact]
    public async Task Send_GetShieldModeStatusRequest_ReturnSuccessResponse()
    {
        await GetShieldModeStatus(_fixture.CreateClient(), _fixture.UserIdentity.UserId, TestContext.Current.CancellationToken);
    }

    private static ValueTask<TwitchResponse<UpdateShieldModeStatusResponse>> UpdateShieldModeStatus(ITwitchClient client, UserId broadcasterId, bool isActive, CancellationToken ct)
        => client.SendAsync(new UpdateShieldModeStatusRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            ShieldModeStatus = new() { IsActive = isActive }
        }, ct);

    private static ValueTask<TwitchResponse<GetShieldModeStatusResponse>> GetShieldModeStatus(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetShieldModeStatusRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId
        }, ct);
}
