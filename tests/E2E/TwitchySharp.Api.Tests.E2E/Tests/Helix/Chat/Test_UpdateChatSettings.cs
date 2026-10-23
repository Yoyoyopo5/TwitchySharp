using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_UpdateChatSettings(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("update-chat-settings");

    [Fact]
    public async Task Send_UpdateChatSettingsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetChatSettingsRequest getSettingsRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
        };

        TwitchResponse<GetChatSettingsResponseContent> getSettingsResponse = await client.SendAsync(getSettingsRequest, TestName, ct);
        Assert.NotNull(getSettingsResponse.Content);
        ChatSettings? settings = getSettingsResponse.Content.Data.FirstOrDefault();

        UpdateChatSettingsRequest updateSettingsRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
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

        TwitchResponse<UpdateChatSettingsResponseContent> updateResponse = await client.SendAsync(updateSettingsRequest, TestName, ct);
        Assert.Null(updateResponse.Error);
        await Task.Delay(100, ct);

        if (settings is not null)
        {
            updateResponse = await client.SendAsync(updateSettingsRequest with
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
            }, TestName, ct);
            Assert.Null(updateResponse.Error);
        } // restore
    }
}
