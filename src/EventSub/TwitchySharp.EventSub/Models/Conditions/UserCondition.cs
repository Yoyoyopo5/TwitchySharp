namespace TwitchySharp.EventSub.Models.Conditions;

/// <summary>
/// An EventSub notification condition with a user id.
/// </summary>
public record UserCondition
{
    /// <summary>
    /// The id of the user the notification is for.
    /// </summary>
    public required string UserId { get; init; }
}
