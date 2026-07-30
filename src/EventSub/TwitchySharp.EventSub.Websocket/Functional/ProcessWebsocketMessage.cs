using TwitchySharp.EventSub.Websocket.Serialization;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// Process a single EventSub Websocket message.
/// </summary>
/// <param name="message">The incoming message stream.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="ValueTask"/> containing a <see cref="Validation"/> of the deserialized <see cref="EventSubWebsocketMessage"/>.</returns>
public delegate ValueTask<Validation<EventSubWebsocketMessage>> ProcessWebsocketMessage(WebsocketMessageStream message, CancellationToken ct);

public static class ProcessWebsocketMessageExtensions
{
    public static ProcessWebsocketMessage With(this ProcessWebsocketMessage pipeline, Func<ProcessWebsocketMessage, ProcessWebsocketMessage> with)
        => with(pipeline);
}
