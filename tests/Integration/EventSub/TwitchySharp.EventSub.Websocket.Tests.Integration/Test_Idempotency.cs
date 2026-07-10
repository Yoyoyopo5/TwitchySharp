using TwitchySharp.EventSub.Websocket.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class Test_Idempotency(WebsocketFixture fixture) : IClassFixture<WebsocketFixture>
{
    private readonly WebsocketFixture _fixture = fixture;

    private static EventSubWebsocketMessage<KeepaliveMessagePayload> CreateKeepalive(WebsocketMessageId messageId)
        => new()
        {
            Metadata = new()
            {
                MessageId = messageId,
                MessageTimestamp = DateTimeOffset.UtcNow,
                MessageType = WebsocketMessageType.Keepalive
            },
            Payload = new()
        };

    [Fact]
    public async Task RecieveUniqueMessageIds_HandlerAcceptsBoth()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromMilliseconds(200));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(CreateKeepalive(new("test-message-1")), ct);
        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(CreateKeepalive(new("test-message-2")), ct);
        await connection.Handler.WaitForMessage(ct);

        Assert.Equal(2, connection.Handler.KeepaliveCounter);
    }

    [Fact]
    public async Task ReceiveSameMessageId_HandlerIgnoresSecondMessage()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromMilliseconds(200));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        EventSubWebsocketMessage<KeepaliveMessagePayload> repeat = CreateKeepalive(new("test-message"));

        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(repeat, ct);
        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(repeat, ct);
        await Task.Delay(25, ct); // Since the message won't be received by handler.

        Assert.Equal(1, connection.Handler.KeepaliveCounter);
    }
}
