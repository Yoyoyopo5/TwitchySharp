namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific fragment of a chat message that was caught by automod.
/// </summary>
[Obsolete] // Try to replace with HeldMessage, but the fragment type is slightly different, delete this if not needed after testing.
public record AutomodMessageUpdateChatMessageFragment
{
    /// <summary>
    /// The message text of the fragment.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The emote of the fragment.
    /// This is <see langword="null"/> if the fragment is not an emote.
    /// </summary>
    public AutomodCaughtChatEmote? Emote { get; init; }
    /// <summary>
    /// The bits cheer emote of the fragment.
    /// This is <see langword="null"/> if the fragment is not a bits cheermote.
    /// </summary>
    public AutomodCaughtCheermote? Cheermote { get; init; }
}
