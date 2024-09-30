using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Channels.Ads.Responses;
/// <summary>
/// Contains information about a channel's ad schedule.
/// </summary>
public record GetAdScheduleResponse
{
    /// <summary>
    /// A list that contains information related to the channel’s ad schedule.
    /// There should only be one entry?
    /// </summary>
    [JsonInclude, JsonRequired]
    public AdSchedule[] Data { get; private set; } = [];
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
    [JsonInclude, JsonRequired]
    public int SnoozeCount { get; private set; }
    /// <summary>
    /// The time when the broadcaster will gain an additional snooze.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateTimeOffset SnoozeRefreshAt { get; private set; }
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    /// <summary>
    /// The time of the broadcaster’s next scheduled ad. Null if the channel has no ad scheduled or is not live.
    /// </summary>
    public DateTimeOffset? NextAdAt { get; private set; }
    /// <summary>
    /// The length in seconds of the scheduled upcoming ad break.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int Duration { get; private set; }
    /// <summary>
    /// The time of the broadcaster’s last ad-break. Null if the channel has not run an ad or is not live.
    /// </summary>
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? LastAdAt { get; private set; }
    /// <summary>
    /// The amount of pre-roll free time remaining for the channel in seconds. The value is 0 if they are currently not pre-roll free.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int PrerollFreeTime { get; private set; }
}
