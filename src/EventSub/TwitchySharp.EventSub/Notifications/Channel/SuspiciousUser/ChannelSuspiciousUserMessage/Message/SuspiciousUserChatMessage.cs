namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific chat message from a suspicious user.
/// </summary>
public record SuspiciousUserChatMessage
{
    /// <summary>
    /// The id of the message.
    /// </summary>
    public required string MessageId { get; init; }
    /// <summary>
    /// The message text.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    public required SuspiciousUserChatMessageFragment[] Fragments { get; init; }
}
