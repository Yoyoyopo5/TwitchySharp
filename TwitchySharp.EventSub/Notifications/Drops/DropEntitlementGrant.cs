using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Drops;
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

/// <summary>
/// Contains information about a specific Twitch drops entitlement grant event.
/// </summary>
public record DropEntitlementGrantEventData
{
    /// <summary>
    /// The id of the organization that owns the category (game) that the drop is for.
    /// </summary>
    public required string OrganizationId { get; init; }
    /// <summary>
    /// The id of the category (game) that the drop is for.
    /// </summary>
    public required string CategoryId { get; init; }
    /// <summary>
    /// The name of the category (name).
    /// </summary>
    public required string CategoryName { get; init; }
    /// <summary>
    /// The Drops campaign the entitlement is associated with.
    /// </summary>
    public required string CampaignId { get; init; }
    /// <summary>
    /// The id of the user who was granted the drop entitlement.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the user who was granted the drop entitlement.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the user who was granted the drop entitlement.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The id of the drop entitlement.
    /// </summary>
    public required string EntitlementId { get; init; }
    /// <summary>
    /// The id of the benefit.
    /// </summary>
    public required string BenefitId { get; init; }
    /// <summary>
    /// The date and time when this drop entitlement was granted on Twitch.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
