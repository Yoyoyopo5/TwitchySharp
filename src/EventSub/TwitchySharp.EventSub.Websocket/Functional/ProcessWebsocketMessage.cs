using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// Process a single EventSub Websocket message.
/// </summary>
/// <param name="message">The incoming message stream.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing a <see cref="Validation"/> of the deserialized <see cref="EventSubWebsocketMessage"/>.</returns>
public delegate ValueTask<Validation<EventSubWebsocketMessage>> ProcessWebsocketMessage(WebsocketMessageStream message, CancellationToken ct);
