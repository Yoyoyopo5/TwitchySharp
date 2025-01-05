using System;

namespace TwitchySharp.Api.Helix.Schedule;

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
