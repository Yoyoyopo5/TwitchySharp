using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_SendChatAnnouncement(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SendChatAnnouncementRequest_ReturnSuccessResponse()
    {
        SendChatAnnouncementRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ModeratorId = _fixture.UserIdentity.UserId,
            Announcement = new()
            {
                Color = ChatAnnouncementColor.Blue,
                Message = "test announcement pls ignore"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
