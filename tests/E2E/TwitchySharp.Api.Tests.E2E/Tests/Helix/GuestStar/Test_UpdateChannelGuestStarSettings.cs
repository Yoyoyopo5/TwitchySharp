using TwitchySharp.Api.Helix.GuestStar;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.GuestStar;

public class Test_UpdateChannelGuestStarSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("update-channel-guest-star-settings");

    [Fact]
    public async Task Send_UpdateChannelGuestStarSettingsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetChannelGuestStarSettingsRequest getRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId
        };

        TwitchResponse<GetChannelGuestStarSettingsResponse> getResponse = await client.SendAsync(getRequest, ct);
        ChannelGuestStarSettings cachedSettings = getResponse.Content.Data.Single();

        UpdateChannelGuestStarSettingsRequest updateRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            Settings = new()
            {
                SlotCount = 4,
                IsBrowserSourceAudioEnabled = false,
                IsModeratorSendLiveEnabled = true,
                RegenerateBrowserSources = false,
                GroupLayout = GuestStarGroupLayout.Tiled
            }
        };

        await client.SendAsync(updateRequest, ct);
        await Task.Delay(100, ct);

        UpdateChannelGuestStarSettingsRequest restoreRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            Settings = new()
            {
                SlotCount = cachedSettings.SlotCount,
                IsBrowserSourceAudioEnabled = cachedSettings.IsBrowserSourceAudioEnabled,
                IsModeratorSendLiveEnabled = cachedSettings.IsModeratorSendLiveEnabled,
                GroupLayout = cachedSettings.GroupLayout
            }
        };

        await client.SendAsync(restoreRequest, ct);
    }
}
