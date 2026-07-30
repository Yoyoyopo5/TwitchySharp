namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/> event.
/// </summary>
public record ChannelChatClearUserMessagesEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the user's chat messages were cleared.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the user whose chat messages were cleared.
    /// </summary>
    public required UserId TargetUserId { get; init; }
    /// <summary>
    /// The display name of the user whose chat messages were cleared.
    /// </summary>
    public required UserName TargetUserName { get; init; }
    /// <summary>
    /// The login (username) of the user whose chat messages were cleared.
    /// </summary>
    public required UserLogin TargetUserLogin { get; init; }
}
