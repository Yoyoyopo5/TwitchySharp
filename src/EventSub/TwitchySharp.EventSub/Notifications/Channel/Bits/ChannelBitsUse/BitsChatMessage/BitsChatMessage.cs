namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a chat message that was part of a specific Bits cheer.
/// </summary>
public record BitsChatMessage
{
    public required string Text { get; init; }
    public required BitsChatMessageFragment[] Fragments { get; init; }
}
