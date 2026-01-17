using System;

namespace TwitchySharp.Api.Models.Helix.Channels.Models;

public record FollowedChannel
{
    /// <summary>
    /// The user ID of the the broadcaster that this user is following.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster’s login name (username).
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The time when the user started following the broadcaster.
    /// </summary>
    public required DateTimeOffset FollowedAt { get; init; }
}
