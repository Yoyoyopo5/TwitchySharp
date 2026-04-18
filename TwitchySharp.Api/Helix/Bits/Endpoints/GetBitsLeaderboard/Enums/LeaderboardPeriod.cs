using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Contains static definitions for possible Bits leaderboard period types.
/// </summary>
/// <param name="Value">The string value of the Bits leaderboard period type.</param>
[Wrapper<string>]
public readonly partial record struct LeaderboardPeriod(string Value)
{
    /// <summary>
    /// A day spans from 00:00:00 on the day specified in StartedAt and runs through 00:00:00 of the next day.
    /// </summary>
    public static LeaderboardPeriod Day { get; } = new("day");
    /// <summary>
    /// A week spans from 00:00:00 on the Monday of the week specified in StartedAt and runs through 00:00:00 of the next Monday.
    /// </summary>
    public static LeaderboardPeriod Week { get; } = new("week");
    /// <summary>
    /// A month spans from 00:00:00 on the first day of the month specified in StartedAt and runs through 00:00:00 of the first day of the next month.
    /// </summary>
    public static LeaderboardPeriod Month { get; } = new("month");
    /// <summary>
    /// A year spans from 00:00:00 on the first day of the year specified in StartedAt and runs through 00:00:00 of the first day of the next year.
    /// </summary>
    public static LeaderboardPeriod Year { get; } = new("year");
    /// <summary>
    /// Default. The lifetime of the broadcaster's channel.
    /// </summary>
    public static LeaderboardPeriod All { get; } = new("all");
}
