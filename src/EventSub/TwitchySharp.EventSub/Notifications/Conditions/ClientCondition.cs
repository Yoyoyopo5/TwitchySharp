namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// An EventSub notification condition with a client id.
/// For user authorization notification types.
/// </summary>
public record ClientCondition
{
    public required ClientId ClientId { get; init; }
}
