using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Contains a list of drops entitlements.
/// </summary>
public record GetDropsEntitlementsResponse
{
    /// <summary>
    /// The list of entitlements.
    /// </summary>
    public required DropsEntitlement[] Data { get; init; }
    /// <summary>
    /// The information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through. 
    /// </summary>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a single drops entitlement.
/// </summary>
public record DropsEntitlement
{
    /// <summary>
    /// The unique id of the entitlement.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The id of the benefit (reward) for the entitlement.
    /// </summary>
    public required string BenefitId { get; init; }
    /// <summary>
    /// The date and time when the entitlement was granted.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }
    /// <summary>
    /// The user id of the user who was granted the entitlement.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The game id of the game the user was playing when the reward was entitled.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The entitlement's fulfillment status.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DropsEntitlementStatus>))]
    public required DropsEntitlementStatus FulfillmentStatus { get; init; }
    /// <summary>
    /// The date and time of when the entitlement was last updated.
    /// </summary>
    public required DateTimeOffset LastUpdated { get; init; }
}

/// <summary>
/// The fulfillment status of a drops entitlement.
/// </summary>
public enum DropsEntitlementStatus
{
    Claimed,
    Fulfilled
}
