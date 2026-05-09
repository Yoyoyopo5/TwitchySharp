using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_DeleteChatMessages(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_DeleteChatMessagesRequest_ReturnSuccessResponse()
    {
        const string TEST_MESSAGE = "test message pls delete";
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;
        UserId broadcasterId = _fixture.UserIdentity.UserId;

        var sendResponse = await SendChatMessage(client, broadcasterId, TEST_MESSAGE, ct);
        MessageId messageId = sendResponse.Content.Data.Single().MessageId;
        await Task.Delay(250, ct);
        await DeleteChatMessage(client, broadcasterId, messageId, ct); // Technically can't delete broadcaster's message but this call still succeeds.
    }

    private static ValueTask<TwitchResponse<SendChatMessageResponse>> SendChatMessage(ITwitchClient client, UserId broadcasterId, string message, CancellationToken ct)
        => client.SendAsync(new SendChatMessageRequest()
        {
            Message = new()
            {
                BroadcasterId = broadcasterId,
                SenderId = broadcasterId,
                Message = message
            }
        }, ct);

    private static ValueTask<TwitchResponse<DeleteChatMessagesResponse>> DeleteChatMessage(ITwitchClient client, UserId broadcasterId, MessageId messageId, CancellationToken ct)
        => client.SendAsync(new DeleteChatMessagesRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            MessageId = messageId
        }, ct);
}
