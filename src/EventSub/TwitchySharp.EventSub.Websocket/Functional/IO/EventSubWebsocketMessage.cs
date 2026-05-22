namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// An EventSub Websocket message with a typed payload.
/// </summary>
/// <typeparam name="TPayload">The payload type.</typeparam>
internal record EventSubWebsocketMessage<TPayload> : EventSubWebsocketMessage
{
    /// <summary>
    /// The Websocket message payload.
    /// </summary>
    public required TPayload Payload { get; init; }
}

/// <summary>
/// An EventSub Websocket message.
/// </summary>
public record EventSubWebsocketMessage
{
    /// <summary>
    /// Metadata that identifies the message.
    /// </summary>
    public required EventSubMessageMetadata Metadata { get; init; }
}
