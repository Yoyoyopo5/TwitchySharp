using TwitchySharp.EventSub.Enums.Events.Channel.Bits;
using TwitchySharp.EventSub.Interfaces;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Models.Events.Channel.Bits;

/// <summary>
/// Contains information about a specific fragment in a Bits cheer chat message.
/// </summary>
public record BitsChatMessageFragment : IChatMessageFragment
{
    public required string Text { get; init; }
    public required BitsChatMessageFragmentType Type { get; init; }
    ValueBackedEnum<string> IChatMessageFragment.Type => Type;
    public BitsChatMessageEmote? Emote { get; init; }
    IChatMessageEmote? IChatMessageFragment.Emote => Emote;
    public BitsChatMessageCheermote? Cheermote { get; init; }
    IChatMessageCheermote? IChatMessageFragment.Cheermote => Cheermote;
}
