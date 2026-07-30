namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific message fragment.
/// </summary>
public record ChannelChatMessageFragment
{
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required ChannelChatMessageFragmentType Type { get; init; }
    public required string Text { get; init; }
    /// <summary>
    /// The cheermote, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Cheermote"/>.
    /// </summary>
    public ChannelChatMessageCheermote? Cheermote { get; init; }
    /// <summary>
    /// The emote, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Emote"/>.
    /// </summary>
    public ChannelChatMessageEmote? Emote { get; init; }
    /// <summary>
    /// The mention, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Mention"/>.
    /// </summary>
    public ChannelChatMessageMention? Mention { get; init; }
}
