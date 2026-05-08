using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_WarnChatUser(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_WarnChatUserRequest_ReturnSuccessResponse()
    {
        const string REASON = "You have been selected for a random test of the warning system.";
        const string WARNED_USER_ID = "52137752";
        UserId userToWarn = new(WARNED_USER_ID);

        WarnChatUserRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ModeratorId = _fixture.UserIdentity.UserId,
            Warning = new()
            {
                Data = new()
                {
                    Reason = REASON,
                    UserId = userToWarn
                }
            }
        };

        await _fixture.CreateClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
