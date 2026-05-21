namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// An EventSub notification condition with a broadcaster and moderator user id.
/// </summary>
public record BroadcasterModeratorCondition : BroadcasterCondition
{
    /// <summary>
    /// The user id of the moderator (or the broadcaster) the notification is for.
    /// </summary>
    public required UserId ModeratorUserId { get; init; }
}
