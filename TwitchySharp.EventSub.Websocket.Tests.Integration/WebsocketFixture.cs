using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class Program { }
public class WebsocketFixture : WebApplicationFactory<Program>
{
    private const int TEST_PORT = 28390;
    private readonly TaskCompletionSource<WebSocket> _serverWebSocket = new();
    public Task<WebSocket> ServerWebSocket => _serverWebSocket.Task;
    public TestHandler Handler => Services.GetRequiredService<IWebsocketEventSubHandler>() as TestHandler ?? throw new InvalidOperationException("The IWebsocketEventSubHandler is not registered as TestHandler.");
    public TwitchEventSubWebsocketClient Client => Services.GetRequiredService<TwitchEventSubWebsocketClient>();
    public static Uri Path => new UriBuilder()
        {
            Host = "localhost",
            Scheme = "ws",
            Port = TEST_PORT
        }.Uri;

    public WebsocketFixture()
    {
        // We use kestrel here because Websocket library requires a WebSocketClient (cannot be used with TestServer as far as I know).
        UseKestrel(TEST_PORT);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((ctx, s) =>
        {
            s.AddSingleton<IWebsocketEventSubHandler, TestHandler>();
            s.AddTransient(sp => new TwitchEventSubWebsocketClient(
                sp.GetRequiredService<IWebsocketEventSubHandler>(),
                Path.ToString()
                ));
        });
        builder.Configure(app =>
        {
            app.UseWebSockets();
            app.Run(async ctx =>
            {
                await Task.Delay(100);
                if (!ctx.WebSockets.IsWebSocketRequest)
                {
                    ctx.Response.StatusCode = 400;
                    return;
                }

                using WebSocket ws = await ctx.WebSockets.AcceptWebSocketAsync();
                _serverWebSocket.TrySetResult(ws); // Allow test methods to send their own messages.

                await ws.WaitForClientClose();
            });
        });
    }

    protected override IHostBuilder? CreateHostBuilder()
        => Host.CreateDefaultBuilder();
}

public class TestHandler : IWebsocketEventSubHandler
{
    public EventSubWebsocketSession? Session { get; private set; }
    public int KeepaliveCounter { get; private set; } = 0;
    public IEventSubNotification? LastNotification { get; private set; }
    public EventSubSubscription? RevokedSubscription { get; private set; }
    public ValueTask OnConnected(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        Session = session;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnException(Exception exception, CancellationToken ct = default)
    {
        throw new Exception("An exception occured.", exception);
    }

    public ValueTask OnKeepalive(CancellationToken ct = default)
    {
        KeepaliveCounter++;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
    {
        LastNotification = notification;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSubscriptionRevoked(EventSubSubscription subscription, CancellationToken ct = default)
    {
        RevokedSubscription = subscription;
        return ValueTask.CompletedTask;
    }
}

public static class WebsocketTestExtensions
{
    public static Task SendAsync(this WebSocket ws, string message, CancellationToken ct = default)
        => ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
            WebSocketMessageType.Text,
            true,
            ct
            );

    public static async Task WaitForClientClose(this WebSocket ws, CancellationToken ct = default)
    {
        byte[] buffer = new byte[1024 * 4];
        while (ws.State == WebSocketState.Open)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), ct);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
            }
        }
    }

    public static Task SendWelcomeMessage(this WebSocket ws, string sessionId, CancellationToken ct = default)
    {
        string WELCOME_MESSAGE = $$"""
            {
              "metadata": {
                "message_id": "96a3f3b5-5dec-4eed-908e-e11ee657416c",
                "message_type": "session_welcome",
                "message_timestamp": "2023-07-19T14:56:51.634234626Z"
              },
              "payload": {
                "session": {
                  "id": "{{sessionId}}",
                  "status": "connected",
                  "connected_at": "2023-07-19T14:56:51.616329898Z",
                  "keepalive_timeout_seconds": 10,
                  "reconnect_url": null
                }
              }
            }
            """;

        return ws.SendAsync(WELCOME_MESSAGE, ct);
    }

    public static Task SendKeepaliveMessage(this WebSocket ws, CancellationToken ct = default)
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

        return ws.SendAsync(KEEPALIVE_MESSAGE, ct);
    }

    public static Task SendReconnectMessage(this WebSocket ws, string url, CancellationToken ct = default)
    {
        string RECONNECT_MESSAGE = $$"""
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
                       "reconnect_url": "ws://{{url}}",
                       "connected_at": "2022-11-16T10:11:12.634234626Z"
                    }
                }
            }
            """;

        return ws.SendAsync(RECONNECT_MESSAGE, ct);
    }

    public static Task SendRevocationMessage(this WebSocket ws, CancellationToken ct = default)
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

        return ws.SendAsync(REVOCATION_MESSAGE, ct);
    }

    public static Task SendNotificationMessage(this WebSocket ws, CancellationToken ct = default)
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

        return ws.SendAsync(NOTIFICATION_MESSAGE, ct);
    }
}
