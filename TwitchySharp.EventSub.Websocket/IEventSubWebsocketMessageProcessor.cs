using TwitchySharp.EventSub.Notifications;
using TwitchySharp.EventSub.Websocket.Messages.Payloads;

namespace TwitchySharp.EventSub.Websocket;
/// <summary>
/// Defines methods for interpreting text data from a Twitch EventSub WebSocket session.
/// Implement this if you want to use a different WebSocket client.
/// </summary>
public interface IEventSubWebsocketMessageProcessor
{
    /// <summary>
    /// Handle a text message received from the WebSocket session.
    /// </summary>
    /// <param name="message">The received message.</param>
    ValueTask HandleMessage(string message, CancellationToken ct = default);
    /// <summary>
    /// Handle a keepalive message.
    /// </summary>
    ValueTask Keepalive(CancellationToken ct = default);
    /// <summary>
    /// Handle a notification message.
    /// </summary>
    /// <param name="notification">The received notification.</param>
    ValueTask Notification(IEventSubNotification notification, CancellationToken ct = default);
    /// <summary>
    /// Handle a revocation message.
    /// </summary>
    /// <param name="subscription">The revoked subscription.</param>
    ValueTask Revocation(EventSubSubscription subscription, CancellationToken ct = default);
    /// <summary>
    /// Handle a welcome message.
    /// </summary>
    /// <param name="session">The session details.</param>
    ValueTask Welcome(EventSubWebsocketSession session, CancellationToken ct = default);
}