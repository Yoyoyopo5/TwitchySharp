namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatMessageDelete"/> event.
/// </summary>
public record ChannelChatMessageDeleteEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the message was deleted.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the message was deleted.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the message was deleted.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user whose message was deleted.
    /// </summary>
    public required UserId TargetUserId { get; init; }
    /// <summary>
    /// The display name of the user whose message was deleted.
    /// </summary>
    public required UserName TargetUserName { get; init; }
    /// <summary>
    /// The login (username) of the user whose message was deleted.
    /// </summary>
    public required UserLogin TargetUserLogin { get; init; }
    /// <summary>
    /// The id of the deleted message.
    /// </summary>
    public required MessageId MessageId { get; init; }
}
