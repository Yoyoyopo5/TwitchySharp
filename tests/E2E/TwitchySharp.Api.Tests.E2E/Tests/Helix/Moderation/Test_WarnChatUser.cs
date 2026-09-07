using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_WarnChatUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("warn-chat-user");

    [Fact]
    public async Task Send_WarnChatUserRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string REASON = "You have been selected for a random test of the warning system.";
        const string WARNED_USER_ID = "88836824"; // Mable
        UserId userToWarn = new(WARNED_USER_ID);

        WarnChatUserRequest request = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
            Warning = new()
            {
                Data = new()
                {
                    Reason = REASON,
                    UserId = userToWarn
                }
            }
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
    }
}
