using TwitchySharp.EventSub.Websocket.Messages.Payloads;

namespace TwitchySharp.EventSub.Websocket;
/// <summary>
/// Implement this interface to define behavior for EventSub websocket messages.
/// </summary>
/// <remarks>
/// If you have no clue what you're doing, create a new class that implements this interface to get started with using EventSub websockets.
/// This is how you define what you want your app to do when it receives EventSub notifications.
/// Pass an instance of your class into a new <see cref="TwitchEventSubWebsocketClient"/> and call <see cref="TwitchEventSubWebsocketClient.StartAsync(CancellationToken)"/>,
/// then these interface methods will be called as messages are received.
/// </remarks>
public interface IWebsocketEventSubHandler : IEventSubHandler
{
    /// <summary>
    /// Called when a welcome message is received from the server.
    /// Note that this can be called multiple times throughout the life of the object due to reconnects.
    /// Be sure to update existing EventSub subscriptions with the updated session id.
    /// </summary>
    /// <param name="session">The current session details.</param>
    ValueTask OnConnected(EventSubWebsocketSession session, CancellationToken ct = default);

    /// <summary>
    /// Called when an exception occurs while processing a message.
    /// </summary>
    /// <param name="exception">The exception.</param>
    ValueTask OnException(Exception exception, CancellationToken ct = default);

    /// <summary>
    /// Called when a keepalive message is recieved from the server.
    /// </summary>
    ValueTask OnKeepalive(CancellationToken ct = default);
    /// <summary>
    /// Called when a reconnect message is recieved from the server.
    /// </summary>
    /// <remarks>
    /// The default <see cref="TwitchEventSubWebsocketClient"/> handles the reconnect process automatically.
    /// Subscriptions do NOT need to be remade after a reconnect, according to Twitch documentation.
    /// This method allows you to detect when a reconnect occurred.
    /// </remarks>
    ValueTask OnReconnected(EventSubReconnectSession reconnect, CancellationToken ct = default);
}
