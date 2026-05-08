using TwitchySharp.EventSub.Models.Events.Drops;

namespace TwitchySharp.EventSub.Models.Notifications.Drops;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.DropEntitlementGrant"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#dropentitlementgrant">Drop Entitlement Grant</see> for more information.
/// </remarks>
public record DropEntitlementGrantNotification : EventSubNotificationWithMultipleEvents<DropEntitlementGrantEvent, DropEntitlementGrantCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.DropEntitlementGrant"/>.
/// </summary>
public record DropEntitlementGrantCondition
{
    /// <summary>
    /// The id of the organization that owns the category (game) on the developer portal.
    /// </summary>
    public required string OrganizationId { get; init; }
    /// <summary>
    /// The id of the category (game) that this notification is for.
    /// </summary>
    public string? CategoryId { get; init; }
    /// <summary>
    /// The id of the drops campaign that this notification is for.
    /// </summary>
    public string? CampaignId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.DropEntitlementGrant"/> event.
/// </summary>
public record DropEntitlementGrantEvent
{
    /// <summary>
    /// The id of the event.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// Contains information about the specific drop entitlement grant. 
    /// </summary>
    public required DropEntitlementGrantEventData Data { get; init; }
}
