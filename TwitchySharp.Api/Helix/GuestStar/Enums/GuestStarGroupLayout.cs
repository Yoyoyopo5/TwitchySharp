using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains static definitions for possible Guest Star group layouts.
/// </summary>
/// <param name="Value">The string value of the layout.</param>
[Wrapper<string>]
public readonly partial record struct GuestStarGroupLayout(string Value)
{
    /// <summary>
    /// All live guests are tiled within the browser source with the same size.
    /// </summary>
    public static GuestStarGroupLayout Tiled { get; } = new("TILED_LAYOUT");
    /// <summary>
    /// All live guests are tiled within the browser source with the same size. 
    /// If there is an active screen share, it is sized larger than the other guests.
    /// </summary>
    public static GuestStarGroupLayout Screenshare { get; } = new("SCREENSHARE_LAYOUT");
    /// <summary>
    /// All live guests are arranged in a horizontal bar within the browser source.
    /// </summary>
    public static GuestStarGroupLayout Horizontal { get; } = new("HORIZONTAL_LAYOUT");
    /// <summary>
    /// All live guests are arranged in a vertical bar within the browser source.
    /// </summary>
    public static GuestStarGroupLayout Vertical { get; } = new("VERTICAL_LAYOUT");
}
