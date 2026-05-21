namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// An EventSub notification condition with only a broadcaster user id.
/// </summary>
public record BroadcasterCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the notification is for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
}
