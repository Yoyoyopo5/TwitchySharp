using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// Contains static definitions for possible video types to filter a <see cref="GetVideosRequest"/> query by.
/// </summary>
/// <param name="Value">Set a custom value (use only if a corresponding static definition does not exist).</param>
public record VideoQueryType(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// All video types (no filter).
    /// </summary>
    public static VideoQueryType All { get; } = new("all");
    /// <summary>
    /// On-demand videos (VODs) of past streams.
    /// </summary>
    public static VideoQueryType Archive { get; } = new("archive");
    /// <summary>
    /// Highlight reels of past streams.
    /// </summary>
    public static VideoQueryType Highlight { get; } = new("highlight");
    /// <summary>
    /// External videos that the broadcaster uploaded using the Video Producer.
    /// </summary>
    public static VideoQueryType Upload { get; } = new("upload");
}
