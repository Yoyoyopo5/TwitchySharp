using TwitchySharp.Tests.E2E;
using TwitchySharp.Api.Helix.Chat;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Chat;

public class Test_PinnedMessages(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    public static readonly TestName TestName = new("pinned-messages");

    [Fact]
    public async Task Send_PinnedChatMessageRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);
        CancellationToken ct = TestContext.Current.CancellationToken;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();

        SendChatMessageRequest sendChatMessageRequest = new()
        {
            Message = new()
            {
                BroadcasterId = userConfig.UserId,
                SenderId = userConfig.UserId,
                Message = "Test pinned message"
            }
        };

        TwitchResponse<SendChatMessageResponseContent> sendChatMessageResponse
            = await client.SendAsync(
                sendChatMessageRequest,
                TestName,
                ct);

        Assert.Null(sendChatMessageResponse.Error);
        Assert.NotNull(sendChatMessageResponse.Content);

        MessageId messageId = sendChatMessageResponse.Content.Data.First().MessageId;

        PinChatMessageRequest pinRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
            MessageId = messageId
        };

        TwitchResponse<PinChatMessageResponseContent> pinResponse
            = await client.SendAsync(pinRequest, TestName, ct);

        Assert.Null(pinResponse.Error);

        GetPinnedChatMessageRequest getRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId
        };

        TwitchResponse<GetPinnedChatMessageResponseContent> getResponse
            = await client.SendAsync(getRequest, TestName, ct);

        Assert.Null(getResponse.Error);

        UpdatePinnedChatMessageRequest updateRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
            MessageId = messageId,
            Duration = TimeSpan.FromSeconds(30)
        };

        TwitchResponse<UpdatePinnedChatMessageResponseContent> updateResponse
            = await client.SendAsync(updateRequest, TestName, ct);

        Assert.Null(updateResponse.Error);

        UnpinChatMessageRequest unpinRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
            MessageId = messageId
        };

        TwitchResponse<UnpinChatMessageResponseContent> unpinResponse
            = await client.SendAsync(unpinRequest, TestName, ct);

        Assert.Null(unpinResponse.Error);
    }

    [Fact]
    public async Task Send_SendChatMessageRequestWithPin_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);
        CancellationToken ct = TestContext.Current.CancellationToken;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();

        SendChatMessageRequest sendChatMessageRequest = new()
        {
            Message = new()
            {
                BroadcasterId = userConfig.UserId,
                SenderId = userConfig.UserId,
                Message = "Test pinned message",
                Pin = true
            }
        };

        TwitchResponse<SendChatMessageResponseContent> sendChatMessageResponse
            = await client.SendAsync(
                sendChatMessageRequest with { AuthenticationContext = sendChatMessageRequest.AuthenticationContext with { UsePriorAuthorization = true } },
                TestName,
                ct);

        Assert.Null(sendChatMessageResponse.Error);
        Assert.NotNull(sendChatMessageResponse.Content);

        UnpinChatMessageRequest unpinRequest = new()
        {
            BroadcasterId = userConfig.UserId,
            ModeratorId = userConfig.UserId,
            MessageId = sendChatMessageResponse.Content.Data.First().MessageId
        };

        TwitchResponse<UnpinChatMessageResponseContent> unpinResponse
            = await client.SendAsync(unpinRequest, TestName, ct);

        Assert.Null(unpinResponse.Error);
    }
}
