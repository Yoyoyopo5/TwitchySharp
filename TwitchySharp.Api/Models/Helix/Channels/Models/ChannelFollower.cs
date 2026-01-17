using System;

namespace TwitchySharp.Api.Models.Helix.Channels.Models;

public record ChannelFollower
{
    /// <summary>
    /// The time when the user started following the broadcaster.
    /// </summary>
    public required DateTimeOffset FollowedAt { get; init; }
    /// <summary>
    /// The user ID of the follower.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The follower's login name (username).
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The follower's display name.
    /// </summary>
    public required string UserName { get; init; }
}
