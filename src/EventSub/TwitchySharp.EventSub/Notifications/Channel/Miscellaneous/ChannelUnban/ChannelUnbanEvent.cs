namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUnban"/> event.
/// </summary>
public record ChannelUnbanEvent
{
    /// <summary>
    /// The id of the user who was unbanned or untimedout.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user who was unbanned or untimedout.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user who was unbanned or untimedout.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the user was unbanned or untimedout.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the user was unbanned or untimedout.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the user was unbanned or untimedout.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator (or the broadcaster) who issued the unban or untimeout.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator (or the broadcaster) who issued the unban or untimeout.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator (or the broadcaster) who issued the unban or untimeout.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
}
