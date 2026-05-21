namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSharedChatSessionEnd"/> event.
/// </summary>
public record ChannelSharedChatEndEvent
{
    /// <summary>
    /// The id of the guest star session.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) from the subscription condition that is no longer active in the shared chat session.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) from the subscription condition that is no longer active in the shared chat session.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) from the subscription condition that is no longer active in the shared chat session.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the shared chat session. 
    /// </summary>
    public required UserId HostBroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the shared chat session.
    /// </summary>
    public required UserName HostBroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the shared chat session.
    /// </summary>
    public required UserLogin HostBroadcasterUserLogin { get; init; }
}
