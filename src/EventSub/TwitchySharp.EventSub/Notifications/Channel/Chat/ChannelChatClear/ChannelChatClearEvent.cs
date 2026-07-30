namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatClear"/> event.
/// </summary>
public record ChannelChatClearEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that had their chat cleared.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that had their chat cleared.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that had their chat cleared.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
}
