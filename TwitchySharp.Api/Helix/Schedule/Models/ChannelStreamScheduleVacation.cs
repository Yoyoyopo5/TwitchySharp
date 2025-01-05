using System;

namespace TwitchySharp.Api.Helix.Schedule;

/// <summary>
/// Contains information about a broadcaster's vacation schedule.
/// </summary>
public readonly record struct ChannelStreamScheduleVacation
{
    /// <summary>
    /// The date and time of when the broadcaster’s vacation starts.
    /// </summary>
    public required DateTimeOffset StartTime { get; init; }
    /// <summary>
    /// The date and time of when the broadcaster’s vacation ends.
    /// </summary>
    public required DateTimeOffset EndTime { get; init; }
}
