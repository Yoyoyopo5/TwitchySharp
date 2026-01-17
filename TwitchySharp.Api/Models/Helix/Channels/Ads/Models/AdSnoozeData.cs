using System;

namespace TwitchySharp.Api.Models.Helix.Channels.Ads.Models;

/// <summary>
/// Contains information related to ad snoozing.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#snooze-next-ad">Snooze Next Ad</see> for more information.
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
