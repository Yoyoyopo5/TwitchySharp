using TwitchySharp.Api.Helix.Extensions;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

[Collection("twitch")]
public class Test_SendExtensionPubSubMessage(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_ExtensionPubSubMessageRequest_ReturnSuccessResponse()
    {
        SendExtensionPubSubMessageRequest globalRequest = new()
        {
            ExtensionIdentity = _fixture.ExtensionIdentity,
            Message = new GlobalPubSubMessageData() { Message = "Test global message." }
        };

        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SendAsync(globalRequest, ct);
    }

    [Fact]
    public async Task Send_BroadcastExtensionPubSubMessageRequest_ReturnSuccessResponse()
    {
        SendExtensionPubSubMessageRequest broadcastRequest = new()
        {
            ExtensionIdentity = _fixture.ExtensionIdentity,
            Message = new BroadcastPubSubMessageData() { Message = "Test broadcast message." }.To(_fixture.UserIdentity.UserId)
        };
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SendAsync(broadcastRequest, ct);
    }

    [Fact]
    public async Task Send_WhisperExtensionPubSubMessageRequest_ReturnSuccessResponse()
    {
        SendExtensionPubSubMessageRequest whisperRequest = new()
        {
            ExtensionIdentity = _fixture.ExtensionIdentity,
            Message = new BroadcastPubSubMessageData() { Message = "Test whisper message." }.WhisperTo(_fixture.UserIdentity.UserId)
        };
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        await client.SendAsync(whisperRequest, ct);
    }
}
