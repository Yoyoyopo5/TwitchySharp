namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific fragment of a suspicious user chat message.
/// </summary>
public record SuspiciousUserChatMessageFragment
{
    /// <summary>
    /// The message fragment text.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required SuspiciousUserChatMessageFragmentType Type { get; init; }
    /// <summary>
    /// The fragment emote, if any.
    /// </summary>
    public SuspiciousUserChatMessageEmote? Emote { get; init; }
    /// <summary>
    /// The fragment cheermote, if any.
    /// </summary>
    public SuspiciousUserChatMessageCheermote? Cheermote { get; init; }
}
