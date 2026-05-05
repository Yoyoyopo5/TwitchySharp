using TwitchySharp.EventSub.Enums.Events.Channel.Bits;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.Bits;

/// <summary>
/// Contains information about a specific fragment in a Bits cheer chat message.
/// </summary>
public record BitsChatMessageFragment : IChatMessageFragment
{
    public required string Text { get; init; }
    public required BitsChatMessageFragmentType Type { get; init; }
    string IChatMessageFragment.Type => Type;
    public BitsChatMessageEmote? Emote { get; init; }
    IChatMessageEmote? IChatMessageFragment.Emote => Emote;
    public BitsChatMessageCheermote? Cheermote { get; init; }
    IChatMessageCheermote? IChatMessageFragment.Cheermote => Cheermote;
    /// <summary>
    /// Not supported for this fragment type.
    /// Set to <see langword="null"/>
    /// </summary>
    IChatMessageMention? IChatMessageFragment.Mention => null;
}
