using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record GetChannelStreamScheduleResponse
{
    /// <summary>
    /// The broadcaster’s streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; } // Interestingly, not an array this time.
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific broadcaster's stream schedule.
/// </summary>
public record ChannelStreamSchedule
{
    /// <summary>
    /// The list of broadcasts in the broadcaster’s streaming schedule.
    /// </summary>
    public required ChannelStreamScheduleSegment[] Segments { get; init; }
    /// <summary>
    /// The user id of the broadcaster that owns the broadcast schedule.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The display name of the broadcaster that owns the broadcast schedule.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster that owns the broadcast schedule.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The dates when the broadcaster is on vacation and not streaming. 
    /// Is set to <see langword="null"/> if vacation mode is not enabled.
    /// </summary>
    public ChannelStreamScheduleVacation? Vacation { get; init; }

}

/// <summary>
/// Contains information about a specific stream schedule segment.
/// </summary>
public record ChannelStreamScheduleSegment
{
    /// <summary>
    /// The id of this broadcast segment.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The date and time when the broadcast starts.
    /// </summary>
    public required DateTimeOffset StartTime { get; init; }
    /// <summary>
    /// The date and time when the broadcast ends.
    /// </summary>
    public required DateTimeOffset EndTime { get; init; }
    /// <summary>
    /// The broadcast segment’s title.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// Indicates whether the broadcaster canceled this segment of a recurring broadcast. 
    /// If the broadcaster canceled this segment, this field is set to the same value as <see cref="EndTime"/>; otherwise, it’s set to <see langword="null"/>.
    /// </summary>
    public DateTimeOffset? CanceledUntil { get; init; }
    /// <summary>
    /// The category that the broadcaster plans to stream or <see langword="null"/> if not specified.
    /// </summary>
    public ChannelStreamScheduleCategory? Category { get; init; }
    /// <summary>
    /// Indicates whether the broadcast is part of a recurring series that streams at the same time each week or is a one-time broadcast. 
    /// Is <see langword="true"/> if the broadcast is part of a recurring series.
    /// </summary>
    public required bool IsRecurring { get; init; }

}

/// <summary>
/// Contains information about a category in a channel's stream schedule.
/// </summary>
public record ChannelStreamScheduleCategory
{
    /// <summary>
    /// The id of the category the broadcaster plans to stream.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the category the broadcaster plans to stream.
    /// </summary>
    public required string Name { get; init; }
}

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
