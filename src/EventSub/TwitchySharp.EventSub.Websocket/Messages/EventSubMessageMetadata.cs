using TwitchySharp.EventSub.Websocket.Messages.Enums;

namespace TwitchySharp.EventSub.Websocket.Messages;

/// <summary>
/// Contains information about a specific Websocket EventSub message.
/// This is sent with every message.
/// </summary>
public record EventSubMessageMetadata
{
    public required string MessageId { get; init; }
    public required EventSubMessageType MessageType { get; init; }
    public required DateTimeOffset MessageTimestamp { get; init; }
    public string? SubscriptionType { get; init; }
    public string? SubscriptionVersion { get; init; }
}
