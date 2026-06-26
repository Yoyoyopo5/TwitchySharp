using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Unit;

public class Test_WithHandler
{
    private class TestHandler : IWebsocketEventSubHandler
    {
        public Error? LastError { get; private set; }
        public bool KeepaliveReceived { get; private set; }
        public IEventSubNotification? LastNotification { get; private set; }
        public EventSubReconnectSession? LastReconnect { get; private set; }
        public EventSubSubscription? LastRevocation { get; private set; }
        public EventSubWebsocketSession? LastWelcome { get; private set; }

        public ValueTask OnError(Error error, CancellationToken ct = default)
        {
            LastError = error;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnKeepalive(CancellationToken ct = default)
        {
            KeepaliveReceived = true;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
        {
            LastNotification = notification;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnReconnect(EventSubReconnectSession reconnect, CancellationToken ct = default)
        {
            LastReconnect = reconnect;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default)
        {
            LastRevocation = revokedSubscription;
            return ValueTask.CompletedTask;
        }
        public ValueTask OnWelcome(EventSubWebsocketSession session, CancellationToken ct = default)
        {
            LastWelcome = session;
            return ValueTask.CompletedTask;
        }
    }

    private static ProcessWebsocketMessage CreateStubProcess(Validation<EventSubWebsocketMessage> stubReturn)
        => (_, _) => ValueTask.FromResult(stubReturn);

    private readonly static EventSubMessageMetadata StubMetadata = new()
    {
        MessageId = new("12345"),
        MessageType = WebsocketMessageType.Notification,
        MessageTimestamp = DateTime.MinValue,
        SubscriptionType = new("fake_subscription"),
        SubscriptionVersion = new("1")
    };

    private class StubNotification : IEventSubNotification
    {
        public EventSubSubscription Subscription { get; } = new()
        {
            Id = new("12345"),
            Status = EventSubSubscriptionStatus.Enabled,
            Cost = 1,
            CreatedAt = DateTime.MinValue,
            Transport = new EventSubTransport()
            {
                Method = EventSubTransportMethod.Websocket,
                SessionId = new("1237")
            },
            Type = new(EventSubSubscriptionTypeNames.CHANNEL_BAN),
            Version = new(EventSubSubscriptionTypeVersions.V1)
        };
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Notification_CallsOnNotified()
    {
        TestHandler handler = new();
        IEventSubNotification expectedNotification = new StubNotification();
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<NotificationMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new NotificationMessagePayload()
            {
                Notification = expectedNotification
            }
        }).WithHandler(handler);

        await mockProcess(new(), CancellationToken.None);

        Assert.Equal(expectedNotification, handler.LastNotification);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Keepalive_CallsOnKeepalive()
    {
        TestHandler handler = new();
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<KeepaliveMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
        }).WithHandler(handler);

        await mockProcess(new(), CancellationToken.None);

        Assert.True(handler.KeepaliveReceived);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Reconnect_CallsOnReconnect()
    {
        TestHandler handler = new();
        EventSubReconnectSession expectedReconnect = new()
        {
            ConnectedAt = DateTime.MinValue,
            Id = new("12378"),
            Status = EventSubSessionStatus.Reconnecting,
            ReconnectUrl = new("wss://fake-ws.com")
        };
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<ReconnectMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
            {
                Session = expectedReconnect
            }
        }).WithHandler(handler);

        await mockProcess(new(), CancellationToken.None);

        Assert.Equal(expectedReconnect, handler.LastReconnect);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Revocation_CallsOnRevoked()
    {
        TestHandler handler = new();
        EventSubSubscription expectedRevocation = new StubNotification().Subscription;
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<RevocationMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
            {
                Subscription = expectedRevocation
            }
        }).WithHandler(handler);

        await mockProcess(new(), CancellationToken.None);

        Assert.Equal(expectedRevocation, handler.LastRevocation);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Welcome_CallsOnWelcome()
    {
        TestHandler handler = new();
        EventSubWebsocketSession expectedWelcome = new()
        {
            Id = new("12378"),
            Status = EventSubSessionStatus.Connected,
            KeepaliveTimeout = TimeSpan.FromSeconds(5),
            ConnectedAt = DateTime.MinValue,
        };
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<WelcomeMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
            {
                Session = expectedWelcome
            }
        }).WithHandler(handler);

        await mockProcess(new(), CancellationToken.None);

        Assert.Equal(expectedWelcome, handler.LastWelcome);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_ProcessReturnsError_CallsOnError()
    {
        TestHandler handler = new();
        Error expectedError = new("test-error");
        ProcessWebsocketMessage mockProcess = CreateStubProcess(expectedError).WithHandler(handler);

        await mockProcess(new(), CancellationToken.None);

        Assert.Equal(expectedError, handler.LastError);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_UnsupportedMessageType_CallsNone()
    {
        TestHandler handler = new();
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<object>()
        {
            Metadata = StubMetadata,
            Payload = new()
        }).WithHandler(handler);

        await mockProcess(new(), CancellationToken.None);

        Assert.Null(handler.LastError);
        Assert.Null(handler.LastNotification);
    }
}
