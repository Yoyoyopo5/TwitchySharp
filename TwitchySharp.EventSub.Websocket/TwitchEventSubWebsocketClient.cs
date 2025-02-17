using System.ComponentModel;
using System.Reactive.Linq;
using System.Text.Json;
using TwitchySharp.EventSub.NotificationConverters;
using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Messages;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;
using TwitchySharp.Shared;
using TwitchySharp.Shared.EventSub.Enums;
using Websocket.Client;

namespace TwitchySharp.EventSub.Websocket;

/// <summary>
/// Abstract class that handles basic Twitch EventSub Websocket message handling and reconnects.
/// To use, derive your own class and override the virtual and abstract methods.
/// </summary>
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
public abstract class TwitchEventSubWebsocketClient(string url = "wss://eventsub.wss.twitch.tv/ws", IWebsocketClient? websocketClient = null, INotificationConverter? converter = null)
    : IDisposable, ISupportInitialize
{
    private readonly IWebsocketClient _ws = websocketClient ?? new WebsocketClient(new Uri(url));
    private readonly INotificationConverter _converter = converter ?? new NotificationConverter();

    /// <summary>
    /// Amount of time to wait after a scheduled keepalive message from Twitch is missed before attempting to reconnect.
    /// </summary>
    public TimeSpan ReconnectGracePeriod { get; set; }

    /// <summary>
    /// Called when a subscription notification is received.
    /// </summary>
    /// <param name="notification">The notification that was received.</param>
    protected abstract ValueTask OnNotified(IEventSubNotification notification);
    /// <summary>
    /// Called when a subscription is revoked.
    /// </summary>
    /// <param name="subscription">
    /// The subscription that was revoked.
    /// See the <see cref="EventSubSubscription.Status"/> property for information about the revocation.
    /// </param>
    protected abstract ValueTask OnSubscriptionRevoked(EventSubSubscription subscription);
    /// <summary>
    /// Called when a welcome message is received from the server.
    /// Note that this can be called multiple times throughout the life of the object due to reconnects.
    /// Be sure to update existing EventSub subscriptions with the updated session id.
    /// </summary>
    /// <param name="session">The current session details.</param>
    protected abstract ValueTask OnConnected(EventSubWebsocketSession session);
    /// <summary>
    /// Called when an exception occurs while processing a message.
    /// </summary>
    /// <param name="exception">The exception.</param>
    protected abstract ValueTask OnException(Exception exception);
    /// <summary>
    /// Called when a keepalive message is recieved from the server.
    /// </summary>
    protected virtual ValueTask OnKeepalive() => ValueTask.CompletedTask;

    private ValueTask HandleMessage(string message, JsonSerializerOptions options)
        => JsonSerializer.Deserialize<EventSubWebsocketMessage<JsonElement>>(message, options) switch
        {
            { Payload.ValueKind: JsonValueKind.Object } esMessage => esMessage switch
            {
                { Metadata.MessageType: EventSubMessageType.Welcome } welcomeMessage => Connected(JsonSerializer.Deserialize<WelcomeMessagePayload>(esMessage.Payload, options)!.Session),
                { Metadata.MessageType: EventSubMessageType.Keepalive } => OnKeepalive(),
                { Metadata.MessageType: EventSubMessageType.Notification, Metadata.SubscriptionType: string, Metadata.SubscriptionVersion: string } notificationMessage => OnNotified(_converter.Deserialize(notificationMessage.Payload, new EventSubSubscriptionType(notificationMessage.Metadata.SubscriptionType, notificationMessage.Metadata.SubscriptionVersion))),
                { Metadata.MessageType: EventSubMessageType.Revocation } revocationMessage => OnSubscriptionRevoked(JsonSerializer.Deserialize<RevocationMessagePayload>(revocationMessage.Payload, options)!.Subscription),
                { Metadata.MessageType: EventSubMessageType.Reconnect } reconnectMessage => HandleReconnect(JsonSerializer.Deserialize<ReconnectMessagePayload>(reconnectMessage.Payload)!.Session),
                _ => ValueTask.CompletedTask
            },
            _ => ValueTask.CompletedTask
        };

    private async ValueTask HandleReconnect(EventSubReconnectSession reconnectSession)
    {
        _ws.Url = new Uri(reconnectSession.ReconnectUrl);
        await _ws.ReconnectOrFail();
    }

    private ValueTask Connected(EventSubWebsocketSession session)
    {
        _ws.ReconnectTimeout = session.KeepaliveTimeout + ReconnectGracePeriod; // Amount of time to wait before reconnecting if no messages received.
        return OnConnected(session);
    }

    public void Dispose()
    {
        _ws.Dispose();
    }

    public void BeginInit()
    {
        _ws.StartOrFail();
        _ws.MessageReceived
            .Where(message => !string.IsNullOrEmpty(message.Text))
            .Subscribe(
                async message => await HandleMessage(message.Text!, JsonConfig.ApiOptions),
                async exception => await OnException(exception)
                );
    }

    public void EndInit() { }
}
