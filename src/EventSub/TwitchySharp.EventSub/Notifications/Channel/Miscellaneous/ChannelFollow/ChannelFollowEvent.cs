namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelFollow"/> event.
/// </summary>
public record ChannelFollowEvent
{
    /// <summary>
    /// The id of the user that followed the channel.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that followed the channel.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that followed the channel.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that was followed.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that was followed.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that was followed.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The date and time when the follow occurred.
    /// </summary>
    public required DateTimeOffset FollowedAt { get; init; }
}
