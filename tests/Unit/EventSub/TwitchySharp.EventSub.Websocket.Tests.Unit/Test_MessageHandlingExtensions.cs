using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Unit;

public class Test_MessageHandlingExtensions
{
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
            Type = EventSubSubscriptionType.ChannelBan.Type,
            Version = EventSubSubscriptionType.ChannelBan.Version
        };
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Notification_CallsOnNotified()
    {
        IEventSubNotification expectedNotification = new StubNotification();
        IEventSubNotification? receivedNotification = null;

        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<NotificationMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new(expectedNotification)
        }).MapNotification<StubNotification>(async (notification, ct) => receivedNotification = notification);

        await mockProcess(new(), TestContext.Current.CancellationToken);

        Assert.Equal(expectedNotification, receivedNotification);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Keepalive_CallsOnKeepalive()
    {
        bool keepaliveReceived = false;
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<KeepaliveMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
        }).MapKeepalive(async _ => keepaliveReceived = true);

        await mockProcess(new(), TestContext.Current.CancellationToken);

        Assert.True(keepaliveReceived);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Reconnect_CallsOnReconnect()
    {
        EventSubReconnectSession expectedReconnect = new()
        {
            ConnectedAt = DateTime.MinValue,
            Id = new("12378"),
            Status = EventSubSessionStatus.Reconnecting,
            ReconnectUrl = new("wss://fake-ws.com")
        };
        EventSubReconnectSession? receivedReconnect = null;
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<ReconnectMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
            {
                Session = expectedReconnect
            }
        }).MapReconnect(async (reconnect, ct) => receivedReconnect = reconnect);

        await mockProcess(new(), TestContext.Current.CancellationToken);

        Assert.Equal(expectedReconnect, receivedReconnect);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Revocation_CallsOnRevoked()
    {
        EventSubSubscription expectedRevocation = new StubNotification().Subscription;
        EventSubSubscription? receivedRevocation = null;

        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<RevocationMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
            {
                Subscription = expectedRevocation
            }
        }).MapSubscriptionRevoked(async (subscription, ct) => receivedRevocation = subscription);

        await mockProcess(new(), TestContext.Current.CancellationToken);

        Assert.Equal(expectedRevocation, receivedRevocation);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_Welcome_CallsOnWelcome()
    {
        EventSubWebsocketSession expectedWelcome = new()
        {
            Id = new("12378"),
            Status = EventSubSessionStatus.Connected,
            KeepaliveTimeout = TimeSpan.FromSeconds(5),
            ConnectedAt = DateTime.MinValue,
        };
        EventSubWebsocketSession? receivedWelcome = null;
        ProcessWebsocketMessage mockProcess = CreateStubProcess(new EventSubWebsocketMessage<WelcomeMessagePayload>()
        {
            Metadata = StubMetadata,
            Payload = new()
            {
                Session = expectedWelcome
            }
        }).MapWelcome(async (welcome, ct) => receivedWelcome = welcome);

        await mockProcess(new(), TestContext.Current.CancellationToken);

        Assert.Equal(expectedWelcome, receivedWelcome);
    }

    [Fact]
    public async Task ProcessWebsocketMessage_ProcessReturnsError_CallsOnError()
    {
        Error expectedError = new("test-error");
        Error? receivedError = null;

        ProcessWebsocketMessage mockProcess = CreateStubProcess(expectedError)
            .MapError(async (error, ct) => receivedError = error);

        await mockProcess(new(), TestContext.Current.CancellationToken);

        Assert.Equal(expectedError, receivedError);
    }
}
