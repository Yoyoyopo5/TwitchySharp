using TwitchySharp.EventSub.Models.Notifications.Channel;

namespace TwitchySharp.EventSub.Websocket.Clients.Websocket.Client.Tests.Integration;

public class Test_WebsocketClientEventSubWebsocketClient(WebsocketFixture fixture) :
    IClassFixture<WebsocketFixture>,
    IAsyncLifetime
{
    private readonly WebsocketFixture _fixture = fixture;
    private WebsocketClientEventSubWebsocketClient _client = default!;

    public async ValueTask InitializeAsync()
    {
        using CancellationTokenSource initTimeout = new(TimeSpan.FromSeconds(3));
        _fixture.Handler.Reset();
        _client = _fixture.Client; // Grab transient.
        await _client.StartAsync(initTimeout.Token);
    }

    public async ValueTask DisposeAsync()
    {
        using CancellationTokenSource disposeTimeout = new(TimeSpan.FromSeconds(3));
        await _client.StopAsync(disposeTimeout.Token);
        _client.Dispose();
    }

    [Fact]
    public async Task ProcessWelcomeMessage_ValidWelcomeMessage_ReturnValidSession()
    {
        const string MOCK_SESSION_ID = "12345";
        const string FAKE_WELCOME_MESSAGE = $$"""
            {
              "metadata": {
                "message_id": "96a3f3b5-5dec-4eed-908e-e11ee657416c",
                "message_type": "session_welcome",
                "message_timestamp": "2023-07-19T14:56:51.634234626Z"
              },
              "payload": {
                "session": {
                  "id": "{{MOCK_SESSION_ID}}",
                  "status": "connected",
                  "connected_at": "2023-07-19T14:56:51.616329898Z",
                  "keepalive_timeout_seconds": 10,
                  "reconnect_url": null
                }
              }
            }
            """;

        using CancellationTokenSource taskTimeout = new(TimeSpan.FromSeconds(5));
        await _fixture.SendTestMessageAsync(FAKE_WELCOME_MESSAGE, taskTimeout.Token);

        Assert.Null(_fixture.Handler.LastException);
        Assert.NotNull(_fixture.Handler.Session);
        Assert.Equal(MOCK_SESSION_ID, _fixture.Handler.Session.Id);
    }

    [Fact]
    public async Task ProcessKeepaliveMessage_ValidKeepaliveMessage_NoException()
    {
        const string FAKE_KEEPALIVE_MESSAGE = """
            {
                "metadata": {
                    "message_id": "84c1e79a-2a4b-4c13-ba0b-4312293e9308",
                    "message_type": "session_keepalive",
                    "message_timestamp": "2023-07-19T10:11:12.634234626Z"
                },
                "payload": {}
            }
            """;

        using CancellationTokenSource taskTimeout = new(TimeSpan.FromSeconds(5));
        await _fixture.SendTestMessageAsync(FAKE_KEEPALIVE_MESSAGE, taskTimeout.Token);

        Assert.Null(_fixture.Handler.LastException);
        Assert.Equal(1, _fixture.Handler.KeepaliveCounter);
    }

    [Fact]
    public async Task ProcessNotificationMessage_ValidNotificationMessage_ReturnNotification()
    {
        const string FAKE_NOTIFICATION_MESSAGE = """
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
                        "type": "channel.follow",
                        "version": "2",
                        "status": "enabled",
                        "cost": 0,
                        "condition": {
                           "broadcaster_user_id": "1337",
                           "moderator_user_id": "1337"
                        },
                         "transport": {
                            "method": "webhook",
                            "callback": "https://example.com/webhooks/callback"
                        },
                        "created_at": "2019-11-16T10:11:12.634234626Z"
                    },
                    "event": {
                        "user_id": "1234",
                        "user_login": "cool_user",
                        "user_name": "Cool_User",
                        "broadcaster_user_id": "1337",
                        "broadcaster_user_login": "cooler_user",
                        "broadcaster_user_name": "Cooler_User",
                        "followed_at": "2020-07-15T18:16:11.17106713Z"
                    }
                }
            }
            """;

        using CancellationTokenSource taskTimeout = new(TimeSpan.FromSeconds(5));
        await _fixture.SendTestMessageAsync(FAKE_NOTIFICATION_MESSAGE, taskTimeout.Token);

        Assert.Null(_fixture.Handler.LastException);
        Assert.NotNull(_fixture.Handler.LastNotification);
        Assert.NotNull(_fixture.Handler.LastNotification as ChannelFollowNotification);
    }

    [Fact]
    public async Task ProcessReconnectMessage_ValidReconnectMessage_ReconnectSuccessful()
    {
        string fakeReconnectMessage = $$"""
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
                       "reconnect_url": "{{WebsocketFixture.Path}}",
                       "connected_at": "2022-11-16T10:11:12.634234626Z"
                    }
                }
            }
            """;

        const string MOCK_SESSION_ID = "12345";
        const string FAKE_WELCOME_MESSAGE = $$"""
            {
              "metadata": {
                "message_id": "96a3f3b5-5dec-4eed-908e-e11ee657416c",
                "message_type": "session_welcome",
                "message_timestamp": "2023-07-19T14:56:51.634234626Z"
              },
              "payload": {
                "session": {
                  "id": "{{MOCK_SESSION_ID}}",
                  "status": "connected",
                  "connected_at": "2023-07-19T14:56:51.616329898Z",
                  "keepalive_timeout_seconds": 10,
                  "reconnect_url": null
                }
              }
            }
            """;

        using CancellationTokenSource taskTimeout = new(TimeSpan.FromSeconds(5));
        await _fixture.SendTestMessageAsync(fakeReconnectMessage, taskTimeout.Token);
        await _fixture.SendTestMessageAsync(FAKE_WELCOME_MESSAGE, taskTimeout.Token);

        Assert.Null(_fixture.Handler.LastException);
        Assert.NotNull(_fixture.Handler.Session);
        Assert.Equal(MOCK_SESSION_ID, _fixture.Handler.Session.Id);
    }

    [Fact]
    public async Task ProcessRevocationMessage_ValidRevocationMessage_ReturnRevokedSubscription()
    {
        const string MOCK_SUBSCRIPTION_ID = "f1c2a387-161a-49f9-a165-0f21d7a4e1c4";
        const string FAKE_REVOCATION_MESSAGE = $$"""
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
                        "id": "{{MOCK_SUBSCRIPTION_ID}}",
                        "status": "authorization_revoked",
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
                    }
                }
            }
            """;

        using CancellationTokenSource taskTimeout = new(TimeSpan.FromSeconds(5));
        await _fixture.SendTestMessageAsync(FAKE_REVOCATION_MESSAGE, taskTimeout.Token);

        Assert.Null(_fixture.Handler.LastException);
        Assert.NotNull(_fixture.Handler.RevokedSubscription);
        Assert.Equal(MOCK_SUBSCRIPTION_ID, _fixture.Handler.RevokedSubscription.Id);
    }
}
