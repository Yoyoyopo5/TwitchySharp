using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.EventSub.Websocket.Idempotency;
using TwitchySharp.EventSub.Websocket.Serialization;
using TwitchySharp.Infrastructure.Functional;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class WebsocketFixture
{
    private readonly ConcurrentDictionary<EventSubWebsocketSessionId, WebSocket> _sockets = [];
    public WebApplication Host { get; }

    public WebsocketFixture()
    {
        Host = ConfigureWebHost(WebApplication.CreateBuilder()).Build();
        Host.UseWebSockets();
        Host.Map("/ws", async ctx =>
        {
            if (!ctx.WebSockets.IsWebSocketRequest)
            {
                ctx.Response.StatusCode = 400;
                return;
            }

            using WebSocket ws = await ctx.WebSockets.AcceptWebSocketAsync();

            // TODO: Create and send welcome message with new session id
            // Add to _sockets with session id key

            await ws.WaitForClientClose();

            // Remove from _sockets
        });
    }

    public Task SendTestMessageAsync(EventSubWebsocketSessionId sessionId, string message, CancellationToken ct = default)
        => !_sockets.TryGetValue(sessionId, out WebSocket? ws)
            ? throw new KeyNotFoundException("The websocket connection with the specified key was not found.")
            : ws.SendAsync(message, ct);

    public Task<StopWebsocketClient> StartNewClient(CancellationToken ct = default)
    {
        using IServiceScope scope = Host.Services.CreateScope();
        return Host.Services.GetRequiredService<StartEventSubWebsocketClient>()(
            Host.Services.GetRequiredService<ProcessWebsocketMessage>(),
            new("ws://localhost/ws"),
            ct
            );
    }

    private static WebApplicationBuilder ConfigureWebHost(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWebsocketEventSubHandler, TestHandler>();
        builder.Services.AddScoped<IdempotencyCache>();
        builder.Services.AddScoped<StartEventSubWebsocketClient>(sp
            => EventSubWebsocketClient.Create(ctx =>
            {
                WebsocketClient client = new(ctx.Uri)
                {
                    IsTextMessageConversionEnabled = false,
                    IsStreamDisposedAutomatically = false
                };
                IDisposable messageHandler = client.MessageReceived.Subscribe(async message =>
                {
                    if (message.Stream is not Stream stream)
                        return;

                    await ctx.OnMessage(stream, TestContext.Current.CancellationToken);
                    await stream.DisposeAsync();
                });
                return async ct =>
                {
                    await client.StartOrFail();
                    return async ct =>
                    {
                        messageHandler.Dispose();
                        await client.StopOrFail(WebSocketCloseStatus.NormalClosure, string.Empty);
                    };
                };
            }).WithReconnects(error => throw error));
        builder.Services.AddScoped<ProcessWebsocketMessage>(sp
            => WebsocketMessageDeserializer.Create()
                .WithIdempotentMessages((id, ct) => sp.GetRequiredService<IdempotencyCache>().IsRepeated(id, ct))
                .WithHandler(sp.GetRequiredService<IWebsocketEventSubHandler>()));
        return builder;
    }
}

public class TestHandler : IWebsocketEventSubHandler
{
    public EventSubWebsocketSession? Session { get; private set; }
    public int KeepaliveCounter { get; private set; } = 0;
    public IEventSubNotification? LastNotification { get; private set; }
    public EventSubSubscription? LastRevokedSubscription { get; private set; }
    public EventSubReconnectSession? LastReconnect { get; private set; }
    public Error? LastError { get; private set; }

    public ValueTask OnWelcome(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        Session = session;
        return ValueTask.CompletedTask;
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
        LastRevokedSubscription = subscription;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnReconnect(EventSubReconnectSession reconnect, CancellationToken ct = default)
    {
        LastReconnect = reconnect;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnError(Error error, CancellationToken ct = default)
    {
        LastError = error;
        return ValueTask.CompletedTask;
    }
}

public class IdempotencyCache
{
    private readonly HashSet<string> _cache = [];

    public ValueTask<bool> IsRepeated(WebsocketMessageId messageId, CancellationToken ct)
    {
        if (_cache.Contains(messageId))
            return ValueTask.FromResult(true);
        _cache.Add(messageId);
        return ValueTask.FromResult(false);
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
            try
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), ct);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
                }
            }
            catch
            {
                await ws.CloseAsync(WebSocketCloseStatus.ProtocolError, "Closing", ct);
            }
        }
    }
}
