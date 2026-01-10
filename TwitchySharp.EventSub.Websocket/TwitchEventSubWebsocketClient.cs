using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text.Json;
using TwitchySharp.EventSub.Interfaces;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Websocket.Messages;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;
using TwitchySharp.Shared;
using TwitchySharp.Shared.EventSub.Enums;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket;

/// <summary>
/// Default Twitch EventSub WebSocket implementation that handles basic Twitch EventSub Websocket message handling and reconnects.
/// Supply your own <see cref="IWebsocketEventSubHandler"/> to recieve events.
/// </summary>
/// <param name="eventSubHandler">
/// The handler that will receive events from the client.
/// </param>
/// <param name="url">
/// The URL of the EventSub server to connect to.
/// Leave this default unless you know what you're doing.
/// </param>
/// <param name="converter">
/// The notification converter to use when receiving subscription notifications.
/// Leave this <see langword="null"/> unless you know what you're doing.
/// </param>
/// <param name="websocketClient">
/// The websocket client to use.
/// Leave this <see langword="null"/> unless you know what you're doing.
/// </param>
/// <param name="messageDeserializerOptions">
/// Custom serializer options to use when deserializing messages.
/// Leave this <see langword="null"/> unless you know what you're doing.
/// </param>
public class TwitchEventSubWebsocketClient(
    IWebsocketEventSubHandler eventSubHandler,
    Uri? websocketUri = null,
    IWebsocketClient? websocketClient = null,
    INotificationConverter? converter = null,
    JsonSerializerOptions? messageDeserializerOptions = null
    )
    : IEventSubWebsocketMessageProcessor, IDisposable, IHostedService
{
    private readonly static Uri DefaultUri = new("wss://eventsub.wss.twitch.tv/ws");
    private readonly IWebsocketClient _ws = websocketClient ?? new WebsocketClient(websocketUri ?? DefaultUri);
    private readonly INotificationConverter _converter = converter ?? new NotificationConverter();
    private readonly IWebsocketEventSubHandler _handler = eventSubHandler;
    private readonly JsonSerializerOptions _serializerOptions = messageDeserializerOptions ?? JsonConfig.ApiOptions;

    /// <summary>
    /// Amount of time to wait after a scheduled keepalive message from Twitch is missed before attempting to reconnect.
    /// </summary>
    public TimeSpan ReconnectGracePeriod { get; set; }

    /// <summary>
    /// Simulate a message received by the websocket.
    /// Useful for testing purposes.
    /// </summary>
    /// <param name="message">The message to recieve.</param>
    public ValueTask HandleMessage(string message, CancellationToken ct = default)
        => ValidateMessage(message, _serializerOptions) switch
        {
            { } valid => ProcessMessage(valid, _serializerOptions, ct),
            _ => ValueTask.CompletedTask
        };

    private static EventSubWebsocketMessage<JsonElement>? ValidateMessage(string message, JsonSerializerOptions? serializerOptions = null)
        => JsonSerializer.Deserialize<EventSubWebsocketMessage<JsonElement>>(message, serializerOptions) switch
        {
            { Payload.ValueKind: not JsonValueKind.Null } valid => valid,
            _ => default
        };

    private ValueTask ProcessMessage(EventSubWebsocketMessage<JsonElement> message, JsonSerializerOptions options, CancellationToken ct = default)
        => message.Metadata.MessageType switch
        {
            EventSubMessageTypes.WELCOME => Welcome(JsonSerializer.Deserialize<WelcomeMessagePayload>(message.Payload, options)!.Session, ct),
            EventSubMessageTypes.KEEPALIVE => Keepalive(ct),
            EventSubMessageTypes.NOTIFICATION => Notification(_converter.Deserialize(message.Payload), ct),
            EventSubMessageTypes.REVOCATION => Revocation(JsonSerializer.Deserialize<RevocationMessagePayload>(message.Payload, options)!.Subscription, ct),
            EventSubMessageTypes.RECONNECT => Reconnect(JsonSerializer.Deserialize<ReconnectMessagePayload>(message.Payload)!.Session, ct),
            _ => ValueTask.CompletedTask
        };

    /// <summary>
    /// Simulate receiving a welcome message from Twitch.
    /// Useful for testing purposes.
    /// </summary>
    /// <param name="session">The session details.</param>
    public ValueTask Welcome(EventSubWebsocketSession session, CancellationToken ct = default)
    {
        _ws.ReconnectTimeout = session.KeepaliveTimeout + ReconnectGracePeriod; // Amount of time to wait before reconnecting if no messages received.
        return _handler.OnConnected(session, ct);
    }

    /// <summary>
    /// Simulate receiving a keepalive message from Twitch.
    /// Useful for testing purposes.
    /// </summary>
    public ValueTask Keepalive(CancellationToken ct = default)
        => _handler.OnKeepalive(ct);

    /// <summary>
    /// Simulate receiving an EventSub notification from Twitch.
    /// Useful for testing purposes.
    /// </summary>
    /// <param name="notification">The notification to receive.</param>
    public ValueTask Notification(IEventSubNotification notification, CancellationToken ct = default)
        => _handler.OnNotified(notification, ct);

    /// <summary>
    /// Simulate receiving an EventSub revocation from Twitch.
    /// Useful for testing purposes.
    /// </summary>
    /// <param name="subscription">The revocation to receive.</param>
    public ValueTask Revocation(EventSubSubscription subscription, CancellationToken ct = default)
        => _handler.OnSubscriptionRevoked(subscription, ct);

    /// <summary>
    /// Simulate receiving a reconnect message from Twitch.
    /// Useful for testing purposes.
    /// </summary>
    /// <param name="session">The reconnect session details.</param>
    internal async ValueTask Reconnect(EventSubReconnectSession session, CancellationToken ct = default)
    {
        _ws.Url = new Uri(session.ReconnectUrl);
        await _ws.ReconnectOrFail();
    }

    public void Dispose()
        => _ws.Dispose();

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (_ws.IsRunning) return;
        _ws.MessageReceived
            .Where(message => !string.IsNullOrEmpty(message.Text))
            .Subscribe(
                async message => await HandleMessage(message.Text!),
                async exception => await _handler.OnException(exception)
                );
        await _ws.StartOrFail();
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
        => _ws.StopOrFail(WebSocketCloseStatus.NormalClosure, string.Empty);
}
