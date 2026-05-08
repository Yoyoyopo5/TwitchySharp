using System;

namespace TwitchySharp.Api.Helix.Entitlements;

/// <summary>
/// Contains information about a single drops entitlement.
/// </summary>
public record DropsEntitlement
{
    /// <summary>
    /// The unique id of the entitlement.
    /// </summary>
    public required DropsEntitlementId Id { get; init; }
    /// <summary>
    /// The id of the benefit (reward) for the entitlement.
    /// </summary>
    public required DropsBenefitId BenefitId { get; init; }
    /// <summary>
    /// The date and time when the entitlement was granted.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }
    /// <summary>
    /// The user id of the user who was granted the entitlement.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The game id of the game the user was playing when the reward was entitled.
    /// </summary>
    public required GameId GameId { get; init; }
    /// <summary>
    /// The entitlement's fulfillment status.
    /// </summary>
    public required DropsEntitlementStatus FulfillmentStatus { get; init; }
    /// <summary>
    /// The date and time of when the entitlement was last updated.
    /// </summary>
    public required DateTimeOffset LastUpdated { get; init; }
}
