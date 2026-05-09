using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_GetSharedChatSession(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetSharedChatSessionRequest_ReturnSuccessResponse()
    {
        // This cannot be fully tested without actually being in a shared chat session.
        GetSharedChatSessionRequest request = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
