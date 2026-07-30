namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShoutoutReceived"/> event.
/// </summary>
public record ShoutoutReceivedEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required UserId FromBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required UserLogin FromBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required UserName FromBroadcasterUserName { get; init; }
    /// <summary>
    /// The number of viewers that were watching the sending broadcaster's stream at the time of the shoutout.
    /// </summary>
    public required int ViewerCount { get; init; }
    /// <summary>
    /// The date and time when the shoutout was sent.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
