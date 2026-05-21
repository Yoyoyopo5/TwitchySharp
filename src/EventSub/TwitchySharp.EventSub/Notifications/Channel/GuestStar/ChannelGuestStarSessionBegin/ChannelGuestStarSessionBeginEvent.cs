namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarSessionBegin"/> event.
/// </summary>
public record ChannelGuestStarSessionBeginEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the Guest Star session that was started.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }
    /// <summary>
    /// The date and time when the Guest Star session began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
