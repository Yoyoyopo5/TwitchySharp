namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.DropEntitlementGrant"/> event.
/// </summary>
public record DropEntitlementGrantEvent
{
    /// <summary>
    /// The id of the entitlement grant event.
    /// </summary>
    public required DropsEntitlementGrantEventId Id { get; init; }
    /// <summary>
    /// Contains information about the specific drop entitlement grant. 
    /// </summary>
    public required DropEntitlementGrantEventData Data { get; init; }
}
