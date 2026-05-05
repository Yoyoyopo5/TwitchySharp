using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Chat;

/// <summary>
/// Contains static definitions for possible message fragment types.
/// </summary>
/// <param name="Value"></param>
[Wrapper<string>]
public readonly partial record struct ChannelChatMessageFragmentType(string Value)
{
    /// <summary>
    /// A plain-text message fragment.
    /// </summary>
    public static ChannelChatMessageFragmentType Text { get; } = new("text");
    /// <summary>
    /// A bits cheer.
    /// </summary>
    public static ChannelChatMessageFragmentType Cheermote { get; } = new("cheermote");
    /// <summary>
    /// An emote.
    /// </summary>
    public static ChannelChatMessageFragmentType Emote { get; } = new("emote");
    /// <summary>
    /// A mention.
    /// </summary>
    public static ChannelChatMessageFragmentType Mention { get; } = new("mention");
}
