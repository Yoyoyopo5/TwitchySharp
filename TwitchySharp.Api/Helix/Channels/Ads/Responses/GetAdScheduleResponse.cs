using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;

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
    /// The time when the broadcaster will gain an additional snooze.
    /// </summary>
    public required DateTimeOffset SnoozeRefreshAt { get; init; }
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    /// <summary>
    /// The time of the broadcaster’s next scheduled ad. Null if the channel has no ad scheduled or is not live.
    /// </summary>
    public DateTimeOffset? NextAdAt { get; init; }
    /// <summary>
    /// The length in seconds of the scheduled upcoming ad break.
    /// </summary>
    public required int Duration { get; init; }
    /// <summary>
    /// The time of the broadcaster’s last ad-break. Null if the channel has not run an ad or is not live.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? LastAdAt { get; init; }
    /// <summary>
    /// The amount of pre-roll free time remaining for the channel in seconds. The value is 0 if they are currently not pre-roll free.
    /// </summary>
    public required int PrerollFreeTime { get; init; }
}
