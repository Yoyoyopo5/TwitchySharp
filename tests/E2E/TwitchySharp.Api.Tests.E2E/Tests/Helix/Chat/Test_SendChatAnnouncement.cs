using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_SendChatAnnouncement(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("send-chat-announcement");

    [Fact]
    public async Task Send_SendChatAnnouncementRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        SendChatAnnouncementRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
            Announcement = new()
            {
                Color = ChatAnnouncementColor.Blue,
                Message = "test announcement pls ignore"
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
