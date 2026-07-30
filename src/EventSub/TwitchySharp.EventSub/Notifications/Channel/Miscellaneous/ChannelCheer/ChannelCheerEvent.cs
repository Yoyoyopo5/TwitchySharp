namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelCheer"/> event.
/// </summary>
public record ChannelCheerEvent
{
    /// <summary>
    /// Indicates whether the cheer was made anonymously.
    /// </summary>
    public required bool IsAnonymous { get; init; }
    /// <summary>
    /// The id of the user that cheered.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserId? UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that cheered.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserLogin? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that cheered.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserName? UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that received the cheer.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the cheer.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the cheer.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The message that was sent with the cheer.
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// The number of Bits cheered.
    /// </summary>
    public required int Bits { get; init; }
}
