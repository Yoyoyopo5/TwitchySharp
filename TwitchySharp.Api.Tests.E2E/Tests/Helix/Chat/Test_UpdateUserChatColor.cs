using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_UpdateUserChatColor(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_UpdateUserChatColor_ReturnSuccessResponse()
    {
        // I would cache original color here but the response is in hex format which only turbo users can update with.
        UpdateUserChatColorRequest request = new()
        {
            UserId = _fixture.UserIdentity.UserId,
            Color = ChatColor.Red
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
