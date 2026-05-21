namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific chat message.
/// </summary>
public record ChannelChatMessage
{
    /// <summary>
    /// The text of the message.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    public required ChannelChatMessageFragment[] Fragments { get; init; }
}
