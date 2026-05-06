namespace TwitchySharp.EventSub.Models.Conditions;

/// <summary>
/// An EventSub notification condition with a client id.
/// For user authorization notification types.
/// </summary>
public record ClientCondition
{
    public required string ClientId { get; init; }
}
