namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// Contains information about a specific Websocket EventSub message.
/// This is sent with every message.
/// </summary>
public record EventSubMessageMetadata
{
    /// <summary>
    /// An id that uniquely identifies the message.
    /// </summary>
    /// <remarks>
    /// Twitch sends messages at least once, but if Twitch is unsure of whether you received a notification, it'll resend the message.
    /// This means you may receive a notification twice. If Twitch resends the message, the message id will be the same.
    /// </remarks>
    public required WebsocketMessageId MessageId { get; init; }
    /// <summary>
    /// The type of message.
    /// </summary>
    public required WebsocketMessageType MessageType { get; init; }
    /// <summary>
    /// The date and time the message was sent.
    /// </summary>
    public required DateTimeOffset MessageTimestamp { get; init; }
    /// <summary>
    /// The type of event sent in the message, if <see cref="MessageType"/> is <see cref="WebsocketMessageType.Notification"/> or <see cref="WebsocketMessageType.Recovation"/>.
    /// </summary>
    public EventSubSubscriptionTypeName? SubscriptionType { get; init; }
    /// <summary>
    /// The version of event sent in the message, if <see cref="MessageType"/> is <see cref="WebsocketMessageType.Notification"/> or <see cref="WebsocketMessageType.Recovation"/>.
    /// </summary>
    public EventSubSubscriptionTypeVersion? SubscriptionVersion { get; init; }
}
