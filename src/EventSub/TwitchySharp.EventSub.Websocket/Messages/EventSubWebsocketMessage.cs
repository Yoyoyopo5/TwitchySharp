namespace TwitchySharp.EventSub.Websocket.Messages;

/// <summary>
/// An EventSub Websocket message with a typed payload.
/// </summary>
/// <typeparam name="TPayload">The payload type.</typeparam>
public record EventSubWebsocketMessage<TPayload> : IEventSubWebsocketMessage
{
    public required EventSubMessageMetadata Metadata { get; init; }
    /// <summary>
    /// The Websocket message payload.
    /// </summary>
    public required TPayload Payload { get; init; }
}

/// <summary>
/// Interface for Twitch EventSub Websocket messages.
/// </summary>
public interface IEventSubWebsocketMessage
{
    /// <summary>
    /// The Websocket message metadata.
    /// </summary>
    EventSubMessageMetadata Metadata { get; }
}
