using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Bits;

/// <summary>
/// Contains static definitions for possible message fragment types on a Bits chat message.
/// </summary>
/// <param name="Value">The string value of the message fragment type.</param>
[Wrapper<string>]
public readonly partial record struct BitsChatMessageFragmentType(string Value)
{
    public static BitsChatMessageFragmentType Text { get; } = new("text");
    public static BitsChatMessageFragmentType Cheermote { get; } = new("cheermote");
    public static BitsChatMessageFragmentType Emote { get; } = new("emote");
}
