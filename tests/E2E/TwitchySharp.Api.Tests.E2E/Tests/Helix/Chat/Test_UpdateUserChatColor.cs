using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_UpdateUserChatColor(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("update-user-chat-color");

    [Fact]
    public async Task Send_UpdateUserChatColor_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        // I would cache original color here but the response is in hex format which only turbo users can update with.
        UpdateUserChatColorRequest request = new()
        {
            UserId = userConfig.UserId,
            Color = ChatColor.Red
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
