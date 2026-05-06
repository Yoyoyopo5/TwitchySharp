namespace TwitchySharp.EventSub.Models.Conditions;

/// <summary>
/// An EventSub notification condition with a broadcaster and moderator user id.
/// </summary>
public record BroadcasterModeratorCondition : BroadcasterCondition
{
    /// <summary>
    /// The user id of the moderator (or the broadcaster) the notification is for.
    /// </summary>
    public required string ModeratorUserId { get; init; }
}
