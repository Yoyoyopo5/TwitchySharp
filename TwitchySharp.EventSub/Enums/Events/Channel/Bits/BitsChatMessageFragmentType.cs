using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Bits;

/// <summary>
/// Contains static definitions for possible message fragment types on a Bits chat message.
/// </summary>
/// <param name="Value">The string value of the message fragment type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<BitsChatMessageFragmentType, string>))]
public record BitsChatMessageFragmentType(string Value) : ValueBackedEnum<string>(Value)
{
    public static BitsChatMessageFragmentType Text { get; } = new("text");
    public static BitsChatMessageFragmentType Cheermote { get; } = new("cheermote");
    public static BitsChatMessageFragmentType Emote { get; } = new("emote");
}
