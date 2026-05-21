namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a chat message that received an automod update.
/// </summary>
[Obsolete] // Try to replace with HeldMessage, but the fragment type is slightly different, delete this if not needed after testing.
public record AutomodMessageUpdateChatMessage
{
    /// <summary>
    /// The content of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// Metadata surrounding the potential inappropriate fragments of the message.
    /// </summary>
    public required AutomodMessageUpdateChatMessageFragment[] Fragments { get; init; }
}
