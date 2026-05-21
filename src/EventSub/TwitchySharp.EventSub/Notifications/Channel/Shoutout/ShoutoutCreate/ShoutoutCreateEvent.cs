namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShoutoutCreate"/> event.
/// </summary>
public record ShoutoutCreateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that sent the shoutout.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required UserId ToBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required UserLogin ToBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the shoutout.
    /// </summary>
    public required UserName ToBroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator that sent the shoutout.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that sent the shoutout.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that sent the shoutout.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The date and time when the broadcaster may send another shoutout.
    /// </summary>
    public required DateTimeOffset CooldownEndsAt { get; init; }
    /// <summary>
    /// The date and time when the broadcaster may send another shoutout to the same broadcaster.
    /// </summary>
    public required DateTimeOffset TargetCooldownEndsAt { get; init; }
    /// <summary>
    /// The number of viewers that were watching the sending broadcaster's stream at the time of the shoutout.
    /// </summary>
    public required int ViewerCount { get; init; }
    /// <summary>
    /// The date and time when the shoutout was sent.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
