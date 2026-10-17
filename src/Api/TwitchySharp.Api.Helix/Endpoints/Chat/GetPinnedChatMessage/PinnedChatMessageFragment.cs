namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// A fragment of a pinned chat message.
/// </summary>
public record PinnedChatMessageFragment
{
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required PinnedChatMessageFragmentType Type { get; init; }
    /// <summary>
    /// The plain text of the fragment.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The cheermote information, if <see cref="Type"/> is <see cref="PinnedChatMessageFragmentType.Cheermote"/>.
    /// </summary>
    public PinnedMessageCheermote? Cheermote { get; init; }
    /// <summary>
    /// The emote information, if <see cref="Type"/> is <see cref="PinnedChatMessageFragmentType.Emote"/>.
    /// </summary>
    public PinnedMessageEmote? Emote { get; init; }
    /// <summary>
    /// The mention information, if <see cref="Type"/> is <see cref="PinnedChatMessageFragmentType.Mention"/>.
    /// </summary>
    public PinnedMessageMention? Mention { get; init; }
}
