using TwitchySharp.EventSub.Websocket.Clients;

namespace TwitchySharp.EventSub.Websocket;
/// <summary>
/// Implement this interface to define side-effect behavior for EventSub websocket messages.
/// </summary>
public interface IWebsocketEventSubHandler : IEventSubHandler
{
    /// <summary>
    /// Called when a welcome message is received from the server.
    /// </summary>
    /// <remarks>
    /// This can be called multiple times throughout the life of the object due to reconnects.
    /// Be sure to update existing EventSub subscriptions with the updated session id.
    /// </remarks>
    /// <param name="session">The current session details.</param>
    ValueTask OnWelcome(EventSubWebsocketSession session, CancellationToken ct = default);

    /// <summary>
    /// Called when a keepalive message is recieved from the server.
    /// </summary>
    ValueTask OnKeepalive(CancellationToken ct = default);

    /// <summary>
    /// Called when a reconnect message is recieved from the server.
    /// </summary>
    /// <remarks>
    /// You can use <see cref="StartEventSubWebsocketClientExtensions.WithReconnects(StartEventSubWebsocketClient, Action{Exception}?)"/>
    /// to enable automatic orchestration of the reconnection handoff at the client level.
    /// </remarks>
    ValueTask OnReconnect(EventSubReconnectSession reconnect, CancellationToken ct = default);
}
