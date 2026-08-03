using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Functional;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class Test_MessageHandlingExtensions(WebsocketFixture fixture) : IClassFixture<WebsocketFixture>
{
    private readonly WebsocketFixture _fixture = fixture;

    [Fact]
    public async Task ReceiveWelcomeMessage_HandlerOnWelcomeCalled()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromSeconds(1));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        await connection.Handler.WaitForMessage(ct);

        Assert.NotNull(connection.Handler.Session);
    }

    [Fact]
    public async Task ReceiveKeepaliveMessage_HandlerOnKeepaliveCalled()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromSeconds(1));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        EventSubWebsocketMessage<KeepaliveMessagePayload> keepalive = new()
        {
            Metadata = new()
            {
                MessageId = new("test-keepalive-message"),
                MessageTimestamp = DateTimeOffset.UtcNow,
                MessageType = WebsocketMessageType.Keepalive
            },
            Payload = new()
        };

        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(keepalive, ct);
        await connection.Handler.WaitForMessage(ct);

        Assert.Equal(1, connection.Handler.KeepaliveCounter);
    }

    [Fact]
    public async Task ReceiveReconnectMessage_HandlerOnReconnectCalled()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromSeconds(1));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        EventSubWebsocketMessage<ReconnectMessagePayload> reconnect = new()
        {
            Metadata = new()
            {
                MessageId = new("test-reconnect-message"),
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

        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(reconnect, ct);
        await connection.Handler.WaitForMessage(ct);

        Assert.NotNull(connection.Handler.LastReconnect);
    }

    [Fact]
    public async Task ReceiveNotificationMessage_HandlerOnNotifiedCalled()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromSeconds(1));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        EventSubWebsocketMessage<IEventSubNotification> notification = new()
        {
            Metadata = new()
            {
                MessageId = new("test-notification-message"),
                MessageTimestamp = DateTimeOffset.UtcNow,
                MessageType = WebsocketMessageType.Notification,
                SubscriptionType = new(EventSubSubscriptionType.ChannelFollow.Type),
                SubscriptionVersion = new(EventSubSubscriptionType.ChannelFollow.Version)
            },
            Payload = new ChannelFollowNotification()
            {
                Subscription = new()
                {
                    Id = new("test-subscription"),
                    Type = new(EventSubSubscriptionType.ChannelFollow.Type),
                    Version = new(EventSubSubscriptionType.ChannelFollow.Version),
                    Status = EventSubSubscriptionStatus.Enabled,
                    Condition = new()
                    {
                        BroadcasterUserId = new("12345"),
                        ModeratorUserId = new("12345")
                    },
                    Cost = 1,
                    CreatedAt = DateTimeOffset.MinValue,
                    Transport = new()
                    {
                        Method = EventSubTransportMethod.Websocket,
                        SessionId = new("test-session")
                    }
                },
                Event = new()
                {
                    BroadcasterUserId = new("12345"),
                    BroadcasterUserLogin = new("testbroadcaster"),
                    BroadcasterUserName = new("TestBroadcaster"),
                    FollowedAt = DateTimeOffset.UtcNow,
                    UserId = new("5678"),
                    UserLogin = new("testuser"),
                    UserName = new("TestUser")
                }
            }
        };

        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(notification, ct);
        await connection.Handler.WaitForMessage(ct);

        Assert.NotNull(connection.Handler.LastNotification);
    }

    [Fact]
    public async Task RecieveRevocationMessage_HandlerOnSubscriptionRevokedCalled()
    {
        CancellationToken ct = TestContext.Current.CreateLinkedCancellationToken(TimeSpan.FromSeconds(1));
        await using WebsocketFixtureExtensions.ConnectionScope connection
            = await _fixture.CreateTestConnection(ct);

        EventSubWebsocketMessage<RevocationMessagePayload> revocation = new()
        {
            Metadata = new()
            {
                MessageId = new("test-revocation-message"),
                MessageTimestamp = DateTimeOffset.UtcNow,
                MessageType = WebsocketMessageType.Recovation,
                SubscriptionType = new(EventSubSubscriptionType.ChannelFollow.Type),
                SubscriptionVersion = new(EventSubSubscriptionType.ChannelFollow.Version)
            },
            Payload = new()
            {
                Subscription = new EventSubSubscription<ChannelFollowCondition>()
                {
                    Id = new("test-subscription"),
                    Type = new(EventSubSubscriptionType.ChannelFollow.Type),
                    Version = new(EventSubSubscriptionType.ChannelFollow.Version),
                    Status = EventSubSubscriptionStatus.Enabled,
                    Condition = new()
                    {
                        BroadcasterUserId = new("12345"),
                        ModeratorUserId = new("12345")
                    },
                    Cost = 1,
                    CreatedAt = DateTimeOffset.MinValue,
                    Transport = new()
                    {
                        Method = EventSubTransportMethod.Websocket,
                        SessionId = new("test-session")
                    }
                }
            }
        };

        await connection.Handler.WaitForMessage(ct);
        await connection.SendMessage(revocation, ct);
        await connection.Handler.WaitForMessage(ct);

        Assert.NotNull(connection.Handler.LastRevokedSubscription);
    }
}
