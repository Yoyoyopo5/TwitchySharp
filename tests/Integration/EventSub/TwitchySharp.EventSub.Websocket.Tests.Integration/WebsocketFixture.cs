using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Clients;
using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.EventSub.Websocket.Idempotency;
using TwitchySharp.EventSub.Websocket.Serialization;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket.Tests.Integration;

public class WebsocketFixture : IAsyncLifetime
{
    private readonly ConcurrentDictionary<EventSubWebsocketSessionId, WebSocket> _sockets = [];
    public IReadOnlyDictionary<EventSubWebsocketSessionId, WebSocket> OpenWebsockets => _sockets;
    public WebApplication Host { get; }

    public IServiceScope NewServiceScope()
        => Host.Services.CreateScope();

    public WebsocketFixture()
    {
        Host = ConfigureWebHost(WebApplication.CreateBuilder()).Build();
        Host.UseWebSockets();
        Host.Map("/ws", async ctx =>
        {
            CancellationToken ct = TestContext.Current.CancellationToken;
            if (!ctx.WebSockets.IsWebSocketRequest)
            {
                ctx.Response.StatusCode = 400;
                return;
            }

            using WebSocket ws = await ctx.WebSockets.AcceptWebSocketAsync();

            EventSubWebsocketSessionId sessionId = new(Guid.NewGuid().ToString());

            _sockets.AddOrUpdate(sessionId, ws, (_, w) => w);

            await ws.SendEventSubMessage(
                new EventSubWebsocketMessage<WelcomeMessagePayload>()
                {
                    Metadata = new()
                    {
                        MessageId = new(Guid.NewGuid().ToString()),
                        MessageTimestamp = DateTimeOffset.UtcNow,
                        MessageType = WebsocketMessageType.Welcome
                    },
                    Payload = new()
                    {
                        Session = new()
                        {
                            Id = sessionId,
                            Status = EventSubSessionStatus.Connected,
                            ConnectedAt = DateTimeOffset.UtcNow,
                            KeepaliveTimeout = TimeSpan.FromSeconds(10)
                        }
                    }
                },
                JsonConfig.ApiOptions,
                ct
                );

            await ws.WaitForClientClose(ct);

            _sockets.TryRemove(new(sessionId, ws));
        });
    }

    public Task SendTestMessageAsync(EventSubWebsocketSessionId sessionId, string message, CancellationToken ct = default)
        => !_sockets.TryGetValue(sessionId, out WebSocket? ws)
            ? throw new KeyNotFoundException("The websocket connection with the specified key was not found.")
            : ws.SendAsync(message, ct);

    public Task SendTestMessageAsync<TPayload>(EventSubWebsocketSessionId sessionId, EventSubWebsocketMessage<TPayload> message, CancellationToken ct = default)
        => SendTestMessageAsync(sessionId, JsonSerializer.Serialize(message, JsonConfig.ApiOptions), ct);

    public async Task<StopWebsocketClient> StartNewClient(IServiceProvider sp, CancellationToken ct = default)
    {
        StartEventSubWebsocketClient start = EventSubWebsocketClient.Create(ctx =>
        {
            Host.GetTestServer().CreateWebSocketClient();
            WebsocketClient client = new(
                url: ctx.Uri,
                connectionFactory: (uri, ct) => Host.GetTestServer().CreateWebSocketClient().ConnectAsync(uri, ct),
                logger: null
                )
            {
                IsStreamDisposedAutomatically = false,
                IsTextMessageConversionEnabled = false
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
                    try
                    {
                        await client.StopOrFail(WebSocketCloseStatus.NormalClosure, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                };
            };
        }).WithReconnects(error => throw error);

        return await start(
                sp.GetRequiredService<ProcessWebsocketMessage>(),
                new("ws://localhost/ws"),
                ct
                );
    }

    private static WebApplicationBuilder ConfigureWebHost(WebApplicationBuilder builder)
    {
        builder.WebHost.UseTestServer();
        builder.Services.AddScoped<TestHandler>();
        builder.Services.AddScoped<IdempotencyCache>();
        builder.Services.AddScoped<ProcessWebsocketMessage>(sp
            => WebsocketMessageDeserializer.Create()
                .WithIdempotentMessages((id, ct) => sp.GetRequiredService<IdempotencyCache>().IsRepeated(id, ct))
                .MapError(async (e, ct) => TestContext.Current.AddAttachment("pipeline-error", $"{e.GetType().FullName}: {e.Message}"))
                .MapError(sp.GetRequiredService<TestHandler>().OnError)
                .MapKeepalive(sp.GetRequiredService<TestHandler>().OnKeepalive)
                .MapReconnect(sp.GetRequiredService<TestHandler>().OnReconnect)
                .MapSubscriptionRevoked(sp.GetRequiredService<TestHandler>().OnSubscriptionRevoked)
                .MapWelcome(sp.GetRequiredService<TestHandler>().OnWelcome)
                .MapNotification<IEventSubNotification>(sp.GetRequiredService<TestHandler>().OnNotified)
                );
        return builder;
    }

    public async ValueTask InitializeAsync()
        => await Host.StartAsync(TestContext.Current.CancellationToken);
    public async ValueTask DisposeAsync()
    {
        await Host.StopAsync();
        await Host.DisposeAsync();
    }
}

public class TestHandler
{
    public EventSubWebsocketSession? Session { get; private set; }
    public int KeepaliveCounter { get; private set; } = 0;
    public IEventSubNotification? LastNotification { get; private set; }
    public EventSubSubscription? LastRevokedSubscription { get; private set; }
    public EventSubReconnectSession? LastReconnect { get; private set; }
    public Error? LastError { get; private set; }

    private TaskCompletionSource MessageReceived = new();

    public async Task WaitForMessage(CancellationToken ct)
    {
        // Not exactly thread safe but the use case shouldn't cause an issue.
        await MessageReceived.Task.WaitAsync(ct);
        MessageReceived = new();
    }

    public ValueTask OnWelcome(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        Session = session;
        MessageReceived.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnKeepalive(CancellationToken ct = default)
    {
        KeepaliveCounter++;
        MessageReceived.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default)
    {
        LastNotification = notification;
        MessageReceived.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSubscriptionRevoked(EventSubSubscription subscription, CancellationToken ct = default)
    {
        LastRevokedSubscription = subscription;
        MessageReceived.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnReconnect(EventSubReconnectSession reconnect, CancellationToken ct = default)
    {
        LastReconnect = reconnect;
        MessageReceived.TrySetResult();
        return ValueTask.CompletedTask;
    }

    public ValueTask OnError(Error error, CancellationToken ct = default)
    {
        LastError = error;
        MessageReceived.TrySetResult();
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
    public static Task SendEventSubMessage<TPayload>(
        this WebSocket ws,
        EventSubWebsocketMessage<TPayload> message,
        JsonSerializerOptions serializerOptions,
        CancellationToken ct = default
        )
        => ws.SendAsync(JsonSerializer.Serialize(message, serializerOptions), ct);

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
