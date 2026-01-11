using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Channels;
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
    private readonly Channel<WebSocket> _sockets = Channel.CreateUnbounded<WebSocket>();
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

    public async Task SendTestMessageAsync(string message, CancellationToken ct = default)
    {
        await (await _sockets.Reader.ReadAsync(ct)).SendAsync(message, ct);
        await Handler.MessageProcessed(ct);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((ctx, s) =>
        {
            s.AddSingleton<IWebsocketEventSubHandler, TestHandler>();
            s.AddTransient(sp => new TwitchEventSubWebsocketClient(
                sp.GetRequiredService<IWebsocketEventSubHandler>(),
                Path
                ));
        });
        builder.Configure(app =>
        {
            app.UseWebSockets();
            app.Run(async ctx =>
            {
                if (!ctx.WebSockets.IsWebSocketRequest)
                {
                    ctx.Response.StatusCode = 400;
                    return;
                }

                using WebSocket ws = await ctx.WebSockets.AcceptWebSocketAsync();
                await _sockets.Writer.WriteAsync(ws);
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
    public Exception? LastException { get; private set; }
    private TaskCompletionSource _messageProcessed = new();
    public void Reset()
    {
        Session = null;
        KeepaliveCounter = 0;
        LastNotification = null;
        RevokedSubscription = null;
        LastException = null;
        _messageProcessed = new();
    }
    public async Task MessageProcessed(CancellationToken ct = default)
    {
        await _messageProcessed.Task.WaitAsync(ct);
        _messageProcessed = new();
        return;
    }

    public ValueTask OnConnected(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        Session = session;
        _messageProcessed.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnException(Exception exception, CancellationToken ct = default)
    {
        LastException = exception;
        _messageProcessed.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnKeepalive(CancellationToken ct = default)
    {
        KeepaliveCounter++;
        _messageProcessed.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
    {
        LastNotification = notification;
        _messageProcessed.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSubscriptionRevoked(EventSubSubscription subscription, CancellationToken ct = default)
    {
        RevokedSubscription = subscription;
        _messageProcessed.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnReconnected(EventSubReconnectSession reconnect, CancellationToken ct = default)
    {
        _messageProcessed.TrySetResult();
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
