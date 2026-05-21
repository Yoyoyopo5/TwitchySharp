namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// An EventSub notification condition with a user id.
/// </summary>
public record UserCondition
{
    /// <summary>
    /// The id of the user the notification is for.
    /// </summary>
    public required UserId UserId { get; init; }
}
