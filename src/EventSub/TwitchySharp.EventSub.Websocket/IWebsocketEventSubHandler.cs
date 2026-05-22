namespace TwitchySharp.EventSub.Websocket;
/// <summary>
/// Implement this interface to define side-effect behavior for EventSub websocket messages.
/// </summary>
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
