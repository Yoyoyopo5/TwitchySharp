namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarSessionEnd"/> event.
/// </summary>
public record ChannelGuestStarSessionEndEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that was in the ended Guest Star session who this subscription is associated with.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that was in the ended Guest Star session who this subscription is associated with..
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that was in the ended Guest Star session who this subscription is associated with..
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the Guest Star session that was ended.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }
    /// <summary>
    /// The date and time when the Guest Star session began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the Guest Star session ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
    /// <summary>
    /// The user id of the broadcaster who started the Guest Star session that ended.
    /// </summary>
    public required UserId HostUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster who started the Guest Star session that ended.
    /// </summary>
    public required UserName HostUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster who started the Guest Star session that ended.
    /// </summary>
    public required UserLogin HostUserLogin { get; init; }
}
