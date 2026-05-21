namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.StreamOffline"/> event.
/// </summary>
public record StreamOfflineEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose stream went offline.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose stream went offline.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose stream went offline.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
}
