using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_ShieldMode(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateShieldModeStatusRequest_ReturnSuccessResponses()
    {
        TestName testName = new("update-shield-mode");

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        UserId broadcasterId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await UpdateShieldModeStatus(client, testName, broadcasterId, true, ct);
        await Task.Delay(250, ct);
        await UpdateShieldModeStatus(client, testName, broadcasterId, false, ct);
    }

    [Fact]
    public async Task Send_GetShieldModeStatusRequest_ReturnSuccessResponse()
    {
        TestName testName = new("get-shield-mode");

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        await GetShieldModeStatus(_fixture.GetTwitchApiClient(), testName, userConfig.UserId, TestContext.Current.CancellationToken);
    }

    private static Task<TwitchResponse<UpdateShieldModeStatusResponseContent>> UpdateShieldModeStatus(TestingTwitchClient client, TestName testName, UserId broadcasterId, bool isActive, CancellationToken ct)
        => client.SendAsync(new UpdateShieldModeStatusRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            ShieldModeStatus = new() { IsActive = isActive }
        }, testName, ct);

    private static Task<TwitchResponse<GetShieldModeStatusResponseContent>> GetShieldModeStatus(TestingTwitchClient client, TestName testName, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetShieldModeStatusRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId
        }, testName, ct);
}
