using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

[Collection("twitch")]
public class Test_SendChatMessage(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_SendChatMessageRequest_ReturnSuccessResponse()
    {
        SendChatMessageRequest request = new()
        {
            Message = new()
            {
                BroadcasterId = _fixture.UserIdentity.UserId,
                SenderId = _fixture.UserIdentity.UserId,
                Message = "test message pls ignore"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task Send_SendChatMessageRequestAsBot_ReturnSuccessResponse()
    {
        SendChatMessageRequest request = new()
        {
            Message = new()
            {
                BroadcasterId = _fixture.UserIdentity.UserId,
                SenderId = _fixture.UserIdentity.UserId,
                Message = "bot mode test message pls ignore"
            }
        };

        await TwitchClientFixture.Client.SendAsync(request.AsBot(_fixture.UserIdentity.ClientId), TestContext.Current.CancellationToken);
    }
}
