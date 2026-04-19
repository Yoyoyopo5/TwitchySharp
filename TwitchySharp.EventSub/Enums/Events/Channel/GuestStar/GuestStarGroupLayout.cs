using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.GuestStar;

/// <summary>
/// Contains static definitions for possible Guest Star group layout types.
/// </summary>
/// <param name="Value">The string value of the layout type.</param>
[Wrapper<string>]
public readonly partial record struct GuestStarGroupLayout(string Value)
{
    /// <summary>
    /// All live guests are tiled within the browser source with the same size. 
    /// </summary>
    public static GuestStarGroupLayout Tiled { get; } = new("tiled");
    /// <summary>
    /// All live guests are tiled within the browser source with the same size. 
    /// If there is an active screen share, it is sized larger than the other guests.
    /// </summary>
    public static GuestStarGroupLayout Screenshare { get; } = new("screenshare");
    /// <summary>
    /// Indicates the group layout will contain all participants in a top-aligned horizontal stack.
    /// </summary>
    public static GuestStarGroupLayout HorizontalTop { get; } = new("horizontal_top");
    /// <summary>
    /// Indicates the group layout will contain all participants in a bottom-aligned horizontal stack.
    /// </summary>
    public static GuestStarGroupLayout HorizontalBottom { get; } = new("horizontal_bottom");
    /// <summary>
    /// Indicates the group layout will contain all participants in a left-aligned vertical stack.
    /// </summary>
    public static GuestStarGroupLayout VerticalLeft { get; } = new("vertical_left");
    /// <summary>
    /// Indicates the group layout will contain all participants in a right-aligned vertical stack.
    /// </summary>
    public static GuestStarGroupLayout VerticalRight { get; } = new("vertical_right");
}
