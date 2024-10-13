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
    public required AdSnoozeData[] Data { get; init; }
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
    public required int SnoozeCount { get; init; }
    /// <summary>
    /// The time when the broadcaster will gain an additional snooze.
    /// </summary>
    public required DateTimeOffset SnoozeRefreshAt { get; init; }
    /// <summary>
    /// The time of the broadcaster’s next scheduled ad.
    /// </summary>
    public required DateTimeOffset NextAdAt { get; init; }
}
