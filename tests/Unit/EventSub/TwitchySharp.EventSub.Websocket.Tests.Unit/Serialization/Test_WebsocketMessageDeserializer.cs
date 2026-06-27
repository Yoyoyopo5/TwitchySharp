using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.EventSub.Websocket.Serialization;
using TwitchySharp.Tests.Unit;
using TwitchySharp.Serialization;
using System.Text.Json;

namespace TwitchySharp.EventSub.Websocket.Tests.Unit.Serialization;

public class Test_WebsocketMessageDeserializer
{
    private record StubNotification : IEventSubNotification
    {
        public EventSubSubscription Subscription { get; }
            = new EventSubSubscription()
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

    private readonly static DeserializeNotification StubNotificationDeserializer
        = (_, _) => ValueTask.FromResult<Validation<IEventSubNotification>>(new StubNotification());

    private readonly static ProcessWebsocketMessage MockProcess = WebsocketMessageDeserializer.Create(
        StubNotificationDeserializer,
        SerializerOptions
        );

    private readonly static JsonSerializerOptions SerializerOptions = JsonConfig.ApiOptions;

    [Fact]
    public async Task ProcessWebsocketMessage_WelcomeMessage_ReturnsWelcomeMessagePayload()
    {
        const string WELCOME_MESSAGE = """
            {
              "metadata": {
                "message_id": "96a3f3b5-5dec-4eed-908e-e11ee657416c",
                "message_type": "session_welcome",
                "message_timestamp": "2023-07-19T14:56:51.634234626Z"
              },
              "payload": {
                "session": {
                  "id": "AQoQILE98gtqShGmLD7AM6yJThAB",
                  "status": "connected",
                  "connected_at": "2023-07-19T14:56:51.616329898Z",
                  "keepalive_timeout_seconds": 10,
                  "reconnect_url": null
                }
              }
            }
            """;

        EventSubWebsocketMessage<WelcomeMessagePayload>? expectedMessage
            = JsonSerializer.Deserialize<EventSubWebsocketMessage<WelcomeMessagePayload>>(WELCOME_MESSAGE, SerializerOptions);

        await MockProcess(new(WELCOME_MESSAGE.ToMemoryStream()), TestContext.Current.CancellationToken)
            .MatchAsync(
            (e, _) => throw new NotSupportedException("Process returned Error (expected EventSubWebsocketMessage)."),
            (message, _) =>
            {
                EventSubWebsocketMessage<WelcomeMessagePayload> welcome = Assert.IsType<EventSubWebsocketMessage<WelcomeMessagePayload>>(message);
                Assert.Equal(expectedMessage, welcome);
                return ValueTask.CompletedTask;
            },
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebsocketMessage_KeepaliveMessage_ReturnsKeepaliveMessagePayload()
    {
        const string KEEPALIVE_MESSAGE = """
            {
                "metadata": {
                    "message_id": "84c1e79a-2a4b-4c13-ba0b-4312293e9308",
                    "message_type": "session_keepalive",
                    "message_timestamp": "2023-07-19T10:11:12.634234626Z"
                },
                "payload": {}
            }
            """;

        EventSubWebsocketMessage<KeepaliveMessagePayload>? expectedMessage
            = JsonSerializer.Deserialize<EventSubWebsocketMessage<KeepaliveMessagePayload>>(KEEPALIVE_MESSAGE, SerializerOptions);

        await MockProcess(new(KEEPALIVE_MESSAGE.ToMemoryStream()), TestContext.Current.CancellationToken)
            .MatchAsync(
            (e, _) => throw new NotSupportedException("Process returned Error (expected EventSubWebsocketMessage)."),
            (message, _) =>
            {
                EventSubWebsocketMessage<KeepaliveMessagePayload> keepalive = Assert.IsType<EventSubWebsocketMessage<KeepaliveMessagePayload>>(message);
                Assert.Equal(expectedMessage, keepalive);
                return ValueTask.CompletedTask;
            },
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebsocketMessage_ReconnectMessage_ReturnsReconnectMessagePayload()
    {
        const string RECONNECT_MESSAGE = """
            {
                "metadata": {
                    "message_id": "84c1e79a-2a4b-4c13-ba0b-4312293e9308",
                    "message_type": "session_reconnect",
                    "message_timestamp": "2022-11-18T09:10:11.634234626Z"
                },
                "payload": {
                    "session": {
                       "id": "AQoQexAWVYKSTIu4ec_2VAxyuhAB",
                       "status": "reconnecting",
                       "keepalive_timeout_seconds": null,
                       "reconnect_url": "wss://eventsub.wss.twitch.tv?...",
                       "connected_at": "2022-11-16T10:11:12.634234626Z"
                    }
                }
            }
            """;

        EventSubWebsocketMessage<ReconnectMessagePayload>? expectedMessage
            = JsonSerializer.Deserialize<EventSubWebsocketMessage<ReconnectMessagePayload>>(RECONNECT_MESSAGE, SerializerOptions);

        await MockProcess(new(RECONNECT_MESSAGE.ToMemoryStream()), TestContext.Current.CancellationToken)
            .MatchAsync(
            (e, _) => throw new NotSupportedException("Process returned Error (expected EventSubWebsocketMessage)."),
            (message, _) =>
            {
                EventSubWebsocketMessage<ReconnectMessagePayload> reconnect = Assert.IsType<EventSubWebsocketMessage<ReconnectMessagePayload>>(message);
                Assert.Equal(expectedMessage, reconnect);
                return ValueTask.CompletedTask;
            },
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebsocketMessage_RevocationMessage_ReturnsRevocationMessagePayload()
    {
        const string REVOCATION_MESSAGE = """
            {

                "metadata": {
                    "message_id": "84c1e79a-2a4b-4c13-ba0b-4312293e9308",
                    "message_type": "revocation",
                    "message_timestamp": "2022-11-16T10:11:12.464757833Z",
                    "subscription_type": "channel.follow",
                    "subscription_version": "1"
                },
                "payload": {
                    "subscription": {
                        "id": "f1c2a387-161a-49f9-a165-0f21d7a4e1c4",
                        "status": "authorization_revoked",
                        "type": "channel.follow",
                        "version": "1",
                        "cost": 1,
                        "condition": null,
                        "transport": {
                            "method": "websocket",
                            "session_id": "AQoQexAWVYKSTIu4ec_2VAxyuhAB"
                        },
                        "created_at": "2022-11-16T10:11:12.464757833Z"
                    }
                }
            }
            """;

        EventSubWebsocketMessage<RevocationMessagePayload>? expectedMessage
            = JsonSerializer.Deserialize<EventSubWebsocketMessage<RevocationMessagePayload>>(REVOCATION_MESSAGE, SerializerOptions);

        await MockProcess(new(REVOCATION_MESSAGE.ToMemoryStream()), TestContext.Current.CancellationToken)
            .MatchAsync(
            (e, _) => throw new NotSupportedException("Process returned Error (expected EventSubWebsocketMessage)."),
            (message, _) =>
            {
                EventSubWebsocketMessage<RevocationMessagePayload> revocation = Assert.IsType<EventSubWebsocketMessage<RevocationMessagePayload>>(message);
                Assert.Equal(expectedMessage, revocation);
                return ValueTask.CompletedTask;
            },
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebsocketMessage_NotificationMessage_ReturnsNotificationMessagePayload()
    {
        const string NOTIFICATION_MESSAGE = """
            {
                "metadata": {
                    "message_id": "befa7b53-d79d-478f-86b9-120f112b044e",
                    "message_type": "notification",
                    "message_timestamp": "2022-11-16T10:11:12.464757833Z",
                    "subscription_type": "channel.follow",
                    "subscription_version": "1"
                },
                "payload": {
                    "subscription": {
                        "id": "f1c2a387-161a-49f9-a165-0f21d7a4e1c4",
                        "status": "enabled",
                        "type": "channel.follow",
                        "version": "1",
                        "cost": 1,
                        "condition": {
                            "broadcaster_user_id": "12826"
                        },
                        "transport": {
                            "method": "websocket",
                            "session_id": "AQoQexAWVYKSTIu4ec_2VAxyuhAB"
                        },
                        "created_at": "2022-11-16T10:11:12.464757833Z"
                    },
                    "event": {
                        "user_id": "1337",
                        "user_login": "awesome_user",
                        "user_name": "Awesome_User",
                        "broadcaster_user_id": "12826",
                        "broadcaster_user_login": "twitch",
                        "broadcaster_user_name": "Twitch",
                        "followed_at": "2023-07-15T18:16:11.17106713Z"
                    }
                }
            }
            """;

        EventSubWebsocketMessage<NotificationMessagePayload>? expectedMessage
            = new()
            {
                Metadata = JsonSerializer.Deserialize<EventSubWebsocketMessage>(NOTIFICATION_MESSAGE, SerializerOptions)!.Metadata,
                Payload = new() { Notification = new StubNotification() }
            };

        await MockProcess(new(NOTIFICATION_MESSAGE.ToMemoryStream()), TestContext.Current.CancellationToken)
            .MatchAsync(
            (e, _) => throw new NotSupportedException("Process returned Error (expected EventSubWebsocketMessage)."),
            (message, _) =>
            {
                EventSubWebsocketMessage<NotificationMessagePayload> notification = Assert.IsType<EventSubWebsocketMessage<NotificationMessagePayload>>(message);
                Assert.Equal(expectedMessage, notification);
                return ValueTask.CompletedTask;
            },
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebsocketMessage_UnsupportedMessageType_ReturnsDeserializationError()
    {
        const string UNSUPPORTED_MESSAGE = """
            {
                "metadata": {
                    "message_id": "84c1e79a-2a4b-4c13-ba0b-4312293e9308",
                    "message_type": "unsupported_message_type",
                    "message_timestamp": "2023-07-19T10:11:12.634234626Z"
                },
                "payload": {}
            }
            """;

        await MockProcess(new(UNSUPPORTED_MESSAGE.ToMemoryStream()), TestContext.Current.CancellationToken)
            .MatchAsync(
            (e, _) =>
            {
                Assert.IsType<WebsocketMessageDeserializer.DeserializationError>(e);
                return ValueTask.CompletedTask;
            },
            (message, _) => throw new NotSupportedException("Process returned EventSubWebsocketMessage (expected Error)."),
            CancellationToken.None
            );
    }

    [Fact]
    public async Task ProcessWebsocketMessage_InvalidWelcomeMessage_ReturnsDeserializationError()
    {
        const string INVALID_WELCOME_MESSAGE = """
            {
              "metadata": {
                "message_id": "96a3f3b5-5dec-4eed-908e-e11ee657416c",
                "message_type": "session_welcome",
                "message_timestamp": "2023-07-19T14:56:51.634234626Z"
              },
              "payload": {
                "session": {
                  "id": 324,
                  "status": "connected",
                  "connected_at": "2023-07-19T14:56:51.616329898Z",
                  "keepalive_timeout_seconds": 10,
                  "reconnect_url": null
                }
              }
            }
            """;

        await MockProcess(new(INVALID_WELCOME_MESSAGE.ToMemoryStream()), TestContext.Current.CancellationToken)
            .MatchAsync(
            (e, _) =>
            {
                Assert.IsType<WebsocketMessageDeserializer.DeserializationError>(e);
                return ValueTask.CompletedTask;
            },
            (message, _) => throw new NotSupportedException("Process returned EventSubWebsocketMessage (expected Error)."),
            CancellationToken.None
            );
    }
}
