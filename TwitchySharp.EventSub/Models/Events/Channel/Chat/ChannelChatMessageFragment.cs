using TwitchySharp.EventSub.Enums.Events.Channel.Chat;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a specific message fragment.
/// </summary>
public record ChannelChatMessageFragment : IChatMessageFragment
{
    /// <summary>
    /// The fragment type.
    /// </summary>
    public required ChannelChatMessageFragmentType Type { get; init; }
    ValueBackedEnum<string> IChatMessageFragment.Type => Type;
    public required string Text { get; init; }
    /// <summary>
    /// The cheermote, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Cheermote"/>.
    /// </summary>
    public ChannelChatMessageCheermote? Cheermote { get; init; }
    IChatMessageCheermote? IChatMessageFragment.Cheermote => Cheermote;
    /// <summary>
    /// The emote, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Emote"/>.
    /// </summary>
    public ChannelChatMessageEmote? Emote { get; init; }
    IChatMessageEmote? IChatMessageFragment.Emote => Emote;
    /// <summary>
    /// The mention, if the <see cref="Type"/> is <see cref="ChannelChatMessageFragmentType.Mention"/>.
    /// </summary>
    public ChannelChatMessageMention? Mention { get; init; }
    IChatMessageMention? IChatMessageFragment.Mention => Mention;
}
