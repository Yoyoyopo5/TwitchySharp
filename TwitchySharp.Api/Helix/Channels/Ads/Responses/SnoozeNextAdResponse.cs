using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Channels.Ads;
/// <summary>
/// Contains information about the snoozed ad.
/// </summary>
public record SnoozeNextAdResponse
{
    /// <summary>
    /// A list that contains information about the channel’s snoozes and next upcoming ad after successfully snoozing.
    /// </summary>
    [JsonInclude, JsonRequired]
    public AdSnoozeData[] Data { get; private set; } = [];
}

/// <summary>
/// Contains information related to ad snoozing.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#snooze-next-ad">snooze next ad</see> for more information.
/// </summary>
public record AdSnoozeData
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
    /// <summary>
    /// The time of the broadcaster’s next scheduled ad.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateTimeOffset NextAdAt { get; private set; }
}
