using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.EventSub.Websocket.Functional;

namespace TwitchySharp.EventSub.Websocket.Idempotency;

/// <summary>
/// Indicates that an EventSub Websocket message was repeated.
/// </summary>
/// <param name="RepeatedMessageId"></param>
public record IdempotencyError(WebsocketMessageId RepeatedMessageId) : Error("A message was repeated.");

public static class ProcessWebsocketMessageExtensions
{
    /// <summary>
    /// Configure a <see cref="ProcessWebsocketMessage"/> pipeline to return an <see cref="IdempotencyError"/> if a <see cref="WebsocketMessageId"/> is repeated between messages.
    /// </summary>
    /// <remarks>
    /// Note that this does not serialize message processing, meaning race conditions are possible.
    /// </remarks>
    /// <param name="pipeline">The processing pipeline to configure.</param>
    /// <param name="isRepeated">The function that determines if the <see cref="WebsocketMessageId"/> is repeated.</param>
    /// <returns>A new <see cref="ProcessWebsocketMessage"/> pipeline configured to return an <see cref="IdempotencyError"/> when a <see cref="WebsocketMessageId"/> is repeated.</returns>
    public static ProcessWebsocketMessage WithIdempotentMessages(
        this ProcessWebsocketMessage pipeline,
        Func<WebsocketMessageId, CancellationToken, ValueTask<bool>> isRepeated
        )
        => pipeline.With(next => (message, ct) => next(message, ct).BindAsync(async (message, ct) => await isRepeated(message.Metadata.MessageId, ct) ? new Validation<EventSubWebsocketMessage>(new IdempotencyError(message.Metadata.MessageId)) : message, ct));
}
