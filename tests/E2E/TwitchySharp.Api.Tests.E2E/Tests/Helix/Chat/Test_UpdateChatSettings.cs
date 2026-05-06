using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_UpdateChatSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateChatSettingsRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetChatSettingsRequest getSettingsRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ModeratorId = _fixture.UserIdentity.UserId,
        };

        var getSettingsResponse = await client.SendAsync(getSettingsRequest, ct);
        ChatSettings? settings = getSettingsResponse.Content.Data.FirstOrDefault();

        UpdateChatSettingsRequest updateSettingsRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ModeratorId = _fixture.UserIdentity.UserId,
            NewSettings = new()
            {
                EmoteMode = true,
                FollowerMode = true,
                SlowMode = true,
                SlowModeWaitTime = TimeSpan.FromSeconds(5),
                FollowerModeDuration = TimeSpan.FromDays(7),
                NonModeratorChatDelay = true,
                NonModeratorChatDelayDuration = TimeSpan.FromSeconds(2),
                SubscriberMode = false,
                UniqueChatMode = false
            }
        };

        await client.SendAsync(updateSettingsRequest, ct);
        await Task.Delay(100, ct);

        if (settings is not null) // restore
            await client.SendAsync(updateSettingsRequest with
            {
                NewSettings = new()
                {
                    EmoteMode = settings.EmoteMode,
                    FollowerMode = settings.FollowerMode,
                    SlowMode = settings.SlowMode,
                    SlowModeWaitTime = settings.SlowModeWaitTime,
                    FollowerModeDuration = settings.FollowerModeDuration,
                    NonModeratorChatDelay = settings.NonModeratorChatDelay,
                    NonModeratorChatDelayDuration = settings.NonModeratorChatDelayDuration,
                    SubscriberMode = settings.SubscriberMode,
                    UniqueChatMode = settings.UniqueChatMode
                }
            }, ct);
    }
}
