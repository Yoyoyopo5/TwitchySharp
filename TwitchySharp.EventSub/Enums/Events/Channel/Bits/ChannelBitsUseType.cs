using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.Bits;

/// <summary>
/// Contains static definitions for possible channel bits use types.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelBitsUseType, string>))]
public record ChannelBitsUseType(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelBitsUseType Cheer { get; } = new("cheer");
    public static ChannelBitsUseType PowerUp { get; } = new("power_up");
}
