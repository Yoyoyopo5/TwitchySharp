using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// Contains static definitions for possible video sorting methods to order a <see cref="GetVideosRequest"/> response by.
/// </summary>
/// <param name="Value">Set a custom value (use only if a corresponding static definition does not exist).</param>
public record VideoQuerySort(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Sorts the returned list in descending order by when they were created (i.e., latest video first).
    /// </summary>
    public static VideoQuerySort Time { get; } = new("time");
    /// <summary>
    /// Sorts the returned list in descending order by biggest gains in viewership (i.e., highest trending video first).
    /// </summary>
    public static VideoQuerySort Trending { get; } = new("trending");
    /// <summary>
    /// Sorts the returned list in descending order by most views (i.e., highest number of views first).
    /// </summary>
    public static VideoQuerySort Views { get; } = new("views");
}
