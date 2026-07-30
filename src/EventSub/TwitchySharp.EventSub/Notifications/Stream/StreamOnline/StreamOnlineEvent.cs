namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.StreamOnline"/> event.
/// </summary>
public record StreamOnlineEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose stream went online.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose stream went online.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose stream went online.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the stream.
    /// </summary>
    public required StreamId Id { get; init; }
    /// <summary>
    /// The stream type.
    /// </summary>
    public required StreamType Type { get; init; }
    /// <summary>
    /// The date and time when the stream went online.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
