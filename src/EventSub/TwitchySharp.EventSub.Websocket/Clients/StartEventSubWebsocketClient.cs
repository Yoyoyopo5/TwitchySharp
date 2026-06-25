using TwitchySharp.EventSub.Websocket.Functional;

namespace TwitchySharp.EventSub.Websocket.Clients;

/// <summary>
/// Start listening to EventSub websocket messages.
/// </summary>
/// <param name="pipeline">The message processing pipeline to use.</param>
/// <param name="url">The websocket url to connect to.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>
/// A <see cref="Task"/> containing a stop function that completes when the client has started listening.
/// </returns>
public delegate Task<StopWebsocketClient> StartEventSubWebsocketClient(ProcessWebsocketMessage pipeline, EventSubWebsocketUrl url, CancellationToken ct = default);
