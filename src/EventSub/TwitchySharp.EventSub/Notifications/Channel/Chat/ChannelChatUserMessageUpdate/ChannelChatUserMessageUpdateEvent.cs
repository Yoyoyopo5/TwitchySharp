namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatUserMessageUpdate"/> event.
/// </summary>
public record ChannelChatUserMessageUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Channel Chat User Message Update notifications for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) to get Channel Chat User Message Update notifications for.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) to get Channel Chat User Message Update notifications for.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that sent the held message.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the held message.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the held message.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The updated status of the held message.
    /// </summary>
    public required ChannelChatUserMessageUpdateStatus Status { get; init; }
    /// <summary>
    /// The id of the held message.
    /// </summary>
    public required MessageId MessageId { get; init; }
    /// <summary>
    /// The held message.
    /// </summary>
    public required ChannelChatMessage Message { get; init; }
}
