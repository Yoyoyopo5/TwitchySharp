namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelVIPRemove"/> event.
/// </summary>
public record ChannelVipRemoveEvent
{
    /// <summary>
    /// The id of the user removed as a VIP.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user removed as a VIP.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user removed as a VIP.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the VIP was removed.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the VIP was removed.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the VIP was removed.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
}
