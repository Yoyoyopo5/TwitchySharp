namespace TwitchySharp.Api.Helix.GuestStar;

/// <summary>
/// Possible values for how guests should be laid out within a browser source in a Guest Star session.
/// </summary>
public enum GuestStarGroupLayout
{
    /// <summary>
    /// All live guests are tiled within the browser source with the same size.
    /// </summary>
    TiledLayout,
    /// <summary>
    /// All live guests are tiled within the browser source with the same size. 
    /// If there is an active screen share, it is sized larger than the other guests.
    /// </summary>
    ScreenshareLayout,
    /// <summary>
    /// All live guests are arranged in a horizontal bar within the browser source.
    /// </summary>
    HorizontalLayout,
    /// <summary>
    /// All live guests are arranged in a vertical bar within the browser source.
    /// </summary>
    VerticalLayout
}
