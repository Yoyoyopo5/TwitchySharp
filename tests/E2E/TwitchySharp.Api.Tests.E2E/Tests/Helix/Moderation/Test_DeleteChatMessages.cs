using TwitchySharp.Api.Helix.Chat;
using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_DeleteChatMessages(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("delete-chat-message");

    [Fact]
    public async Task Send_DeleteChatMessagesRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TEST_MESSAGE = "test message pls delete";
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;
        UserId broadcasterId = userConfig.UserId;

        TwitchResponse<SendChatMessageResponseContent> sendResponse = await SendChatMessage(client, broadcasterId, TEST_MESSAGE, ct);
        MessageId messageId = sendResponse.Content.Data.Single().MessageId;
        await Task.Delay(250, ct);
        await DeleteChatMessage(client, broadcasterId, messageId, ct); // Technically can't delete broadcaster's message but this call still succeeds.
    }

    private static Task<TwitchResponse<SendChatMessageResponseContent>> SendChatMessage(TestingTwitchClient client, UserId broadcasterId, string message, CancellationToken ct)
        => client.SendAsync(new SendChatMessageRequest()
        {
            Message = new()
            {
                BroadcasterId = broadcasterId,
                SenderId = broadcasterId,
                Message = message
            }
        }, TestName, ct);

    private static Task<TwitchResponse<DeleteChatMessagesResponseContent>> DeleteChatMessage(TestingTwitchClient client, UserId broadcasterId, MessageId messageId, CancellationToken ct)
        => client.SendAsync(new DeleteChatMessagesRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            MessageId = messageId
        }, TestName, ct);
}
