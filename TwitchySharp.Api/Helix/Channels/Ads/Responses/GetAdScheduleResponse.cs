using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Helpers.JsonConverters.DateTime;

namespace TwitchySharp.Api.Helix.Channels.Ads;
/// <summary>
/// Contains information about a channel's ad schedule.
/// </summary>
public record GetAdScheduleResponse
{
    /// <summary>
    /// A list that contains information related to the channel’s ad schedule.
    /// There should only be one entry?
    /// </summary>
    public required AdSchedule[] Data { get; init; }
}

/// <summary>
/// Represents the current ad schedule of a broadcaster.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-ad-schedule">get ad schedule</see> for more information.
/// </summary>
public record AdSchedule
{
    /// <summary>
    /// The number of snoozes available for the broadcaster.
    /// </summary>
    public required int SnoozeCount { get; init; }
    /// <summary>
    /// The time when the broadcaster will gain an additional snooze. <see cref="DateTimeOffset.MinValue"> if the channel has no ad scheduled or is not live.
    /// </summary>
    [JsonConverter(typeof(UnixSecondsDateTimeOffsetConverter))]
    public required DateTimeOffset SnoozeRefreshAt { get; init; }
    /// <summary>
    /// The time of the broadcaster’s next scheduled ad. <see cref="DateTimeOffset.MinValue"> if the channel has no ad scheduled.
    /// </summary>
    [JsonConverter(typeof(UnixSecondsDateTimeOffsetConverter))]
    public required DateTimeOffset NextAdAt { get; init; }
    /// <summary>
    /// The length in seconds of the scheduled upcoming ad break.
    /// </summary>
    public required int Duration { get; init; }
    /// <summary>
    /// The time of the broadcaster’s last ad-break. <see cref="DateTimeOffset.MinValue"> if the channel has not run an ad or is not live.
    /// </summary>
    [JsonConverter(typeof(UnixSecondsDateTimeOffsetConverter))]
    public required DateTimeOffset LastAdAt { get; init; }
    /// <summary>
    /// The amount of pre-roll free time remaining for the channel in seconds. The value is 0 if they are currently not pre-roll free.
    /// </summary>
    public required int PrerollFreeTime { get; init; }
}
