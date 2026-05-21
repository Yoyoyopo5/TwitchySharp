using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for possible channel bits use types.
/// </summary>
/// <param name="Value"></param>
[Wrapper<string>]
public readonly partial record struct ChannelBitsUseType(string Value)
{
    public static ChannelBitsUseType Cheer { get; } = new("cheer");
    public static ChannelBitsUseType PowerUp { get; } = new("power_up");
}
