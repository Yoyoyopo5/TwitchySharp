namespace TwitchySharp.Api.Helix.Channels;

public record FollowedChannel
{
    /// <summary>
    /// The user ID of the the broadcaster that this user is following.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster’s login name (username).
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <summary>
    /// The time when the user started following the broadcaster.
    /// </summary>
    public required DateTimeOffset FollowedAt { get; init; }
}
