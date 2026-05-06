using TwitchySharp.Api.Helix.GuestStar;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.GuestStar;

[Collection("twitch")]
public class Test_UpdateChannelGuestStarSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateChannelGuestStarSettingsRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetChannelGuestStarSettingsRequest getRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ModeratorId = _fixture.UserIdentity.UserId
        };

        var getResponse = await client.SendAsync(getRequest, ct);
        ChannelGuestStarSettings cachedSettings = getResponse.Content.Data.Single();

        UpdateChannelGuestStarSettingsRequest updateRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
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
            BroadcasterId = _fixture.UserIdentity.UserId,
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
