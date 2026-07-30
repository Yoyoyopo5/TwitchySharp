namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSuspiciousUserUpdate"/> event.
/// </summary>
public record ChannelSuspiciousUserUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the suspicious user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the suspicious user.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the suspicious user.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The current status of the suspicious user as set by a moderator.
    /// </summary>
    public required SuspiciousUserStatus LowTrustStatus { get; init; }
    /// <summary>
    /// The user id of the moderator that updated the treatment for the suspicious user.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The display name of the moderator that updated the treatment for the suspicious user.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The login (username) of the moderator that updated the treatment for the suspicious user.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
}
