using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// Contains static definitions for possible video periods to filter a <see cref="GetVideosRequest"/> query by.
/// </summary>
/// <param name="Value">Set a custom value (use only if a corresponding static definition does not exist).</param>
[Wrapper<string>]
public readonly partial record struct VideoQueryPeriod(string Value)
{
    /// <summary>
    /// All published videos regardless of publishing time.
    /// </summary>
    public static VideoQueryPeriod All { get; } = new("all");
    /// <summary>
    /// Videos published in the last day.
    /// </summary>
    public static VideoQueryPeriod Day { get; } = new("day");
    /// <summary>
    /// Videos published in the last month.
    /// </summary>
    public static VideoQueryPeriod Month { get; } = new("month");
    /// <summary>
    /// Videos published in the last week.
    /// </summary>
    public static VideoQueryPeriod Week { get; } = new("week");
}
