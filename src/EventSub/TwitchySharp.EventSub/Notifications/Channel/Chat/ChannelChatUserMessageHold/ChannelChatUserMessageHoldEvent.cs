namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatUserMessageHold"/> event.
/// </summary>
public record ChannelChatUserMessageHoldEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the message was sent in.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the message was sent in.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the message was sent in.
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
    /// The id of the message that was held.
    /// </summary>
    public required MessageId MessageId { get; init; }
    /// <summary>
    /// The message that was held.
    /// </summary>
    public required ChannelChatMessage Message { get; init; }
}
