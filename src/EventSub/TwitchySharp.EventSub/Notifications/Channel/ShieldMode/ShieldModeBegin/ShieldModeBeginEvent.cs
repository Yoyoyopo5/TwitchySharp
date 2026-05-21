namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShieldModeBegin"/> event.
/// </summary>
public record ShieldModeBeginEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator who changed the Shield Mode status.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who changed the Shield Mode status.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who changed the Shield Mode status.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The date and time when Shield Mode was enabled.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
