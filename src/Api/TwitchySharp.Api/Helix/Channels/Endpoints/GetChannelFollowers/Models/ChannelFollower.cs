using System;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;

public record ChannelFollower
{
    /// <summary>
    /// The time when the user started following the broadcaster.
    /// </summary>
    public required DateTimeOffset FollowedAt { get; init; }
    /// <summary>
    /// The user ID of the follower.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The follower's login name (username).
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The follower's display name.
    /// </summary>
    public required UserName UserName { get; init; }
}
