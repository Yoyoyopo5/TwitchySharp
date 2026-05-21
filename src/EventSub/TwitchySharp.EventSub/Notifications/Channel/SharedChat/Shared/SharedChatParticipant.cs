namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific broadcaster participant in a shared chat session.
/// </summary>
public record SharedChatParticipant
{
    /// <summary>
    /// The user id of the broadcaster (channel) that is participating in the shared chat session.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is participating in the shared chat session.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is participating in the shared chat session.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
}
