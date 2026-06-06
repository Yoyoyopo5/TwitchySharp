using TwitchySharp.EventSub.Websocket.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Clients;

/// <summary>
/// Start listening to EventSub websocket messages, blocking until <paramref name="ct"/> is cancelled.
/// </summary>
/// <param name="pipeline">The message processing pipeline to use.</param>
/// <param name="url">The websocket url to connect to.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>
/// A <see cref="Task"/> that runs  until <paramref name="ct"/> is cancelled or an exception is thrown.
/// </returns>
public delegate Task ListenToEventSubWebsocketClient(ProcessWebsocketMessage pipeline, EventSubWebsocketUrl url, CancellationToken ct); // Cancellation token handles entire lifetime
