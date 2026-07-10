using TwitchySharp.EventSub.Websocket.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class Test_Reconnects(WebsocketFixture fixture) : IClassFixture<WebsocketFixture>
{
    private readonly WebsocketFixture _fixture = fixture;

    private static EventSubWebsocketMessage<ReconnectMessagePayload> CreateReconnectMessage(WebsocketMessageId? id = null)
        => new()
        {
            Metadata = new()
            {
                MessageId = id ?? new(Guid.NewGuid().ToString()),
                MessageTimestamp = DateTimeOffset.UtcNow,
                MessageType = WebsocketMessageType.Reconnect
            },
            Payload = new()
            {
                Session = new()
                {
                    ConnectedAt = DateTimeOffset.MinValue,
                    Status = EventSubSessionStatus.Reconnecting,
                    Id = new(string.Empty),
                    ReconnectUrl = new("https://reconnect.com/ws")
                }
            }
        };

    [Fact]
    public async Task ReconnectRecieved_SecondConnectionOpened()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromMilliseconds(200));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        EventSubWebsocketMessage<ReconnectMessagePayload> reconnect = CreateReconnectMessage();

        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(reconnect, ct);
        await connection.Handler.WaitForMessage(ct);
        await connection.Handler.WaitForMessage(ct);

        Assert.Equal(2, _fixture.OpenWebsockets.Count);
    }

    [Fact]
    public async Task MultipleReconnectsRecieved_ThenStop_AllConnectionsClosed()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromMilliseconds(500));
        WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        await connection.Handler.WaitForMessage(ct);

        foreach (EventSubWebsocketMessage<ReconnectMessagePayload> reconnect in Enumerable.Range(0, 16).Select(i => CreateReconnectMessage(new(i.ToString()))))
        {
            await connection.SendMessage(reconnect, ct);
        }

        await connection.DisposeAsync();
        await Task.Delay(25, ct); // Wait for clients to close.

        Assert.Empty(_fixture.OpenWebsockets);
    }

    [Fact]
    public async Task MultipleReconnectsReceived_MaxTwoConnectionsOpen()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromMilliseconds(200));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        await connection.Handler.WaitForMessage(ct);

        foreach (EventSubWebsocketMessage<ReconnectMessagePayload> reconnect in Enumerable.Range(0, 16).Select(i => CreateReconnectMessage(new(i.ToString()))))
        {
            await connection.SendMessage(reconnect, ct);
        }

        Assert.True(_fixture.OpenWebsockets.Count <= 2);
    }
}
