using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_UpdateAutoModSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateAutoModSettingsOverallRequest_ReturnSuccessResponse()
    {
        TestName testName = new("update-overall-auto-mod-settings");

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        await UpdateAutoModSettings(_fixture.GetTwitchApiClient(), userConfig.UserId, new UpdateAutoModOverallLevelData(AutomodFilteringLevel.Less), TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_UpdateAutoModSettingsCustomRequest_ReturnSuccessResponse()
    {
        TestName testName = new("update-custom-auto-mod-settings");

        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(testName);

        await UpdateAutoModSettings(_fixture.GetTwitchApiClient(), userConfig.UserId, new UpdateAutoModCustomLevelsData()
        {
            Aggression = AutomodFilteringLevel.Less,
            Swearing = AutomodFilteringLevel.None
        }, TestContext.Current.CancellationToken);
    }

    private static Task<TwitchResponse<UpdateAutoModSettingsResponse>> UpdateAutoModSettings(ITwitchClient client, UserId broadcasterId, UpdateAutoModSettingsRequestData settings, CancellationToken ct)
        => client.SendAsync(new UpdateAutoModSettingsRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            Settings = settings
        }, ct);
}
