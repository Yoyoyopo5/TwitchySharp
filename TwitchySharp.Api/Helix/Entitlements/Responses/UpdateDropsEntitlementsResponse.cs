using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Contains a list of updated entitlements.
/// </summary>
public record UpdateDropsEntitlementsResponse
{
    /// <summary>
    /// A list of entitlements that were updated, grouped by <see cref="EntitlementUpdateStatus"/>.
    /// </summary>
    public required UpdatedEntitlements[] Data { get; init; }
}

/// <summary>
/// Contains information on a group of entitlements updated with a specific <see cref="EntitlementUpdateStatus"/>.
/// </summary>
public record UpdatedEntitlements
{
    /// <summary>
    /// The status of the update.
    /// </summary>
    public required EntitlementUpdateStatus Status { get; init; }
    /// <summary>
    /// The ids of the entitlements that were updated.
    /// </summary>
    public required string[] Ids { get; init; }
}

/// <summary>
/// Possible statuses of an entitlement update.
/// </summary>
public enum EntitlementUpdateStatus
{
    /// <summary>
    /// The entitlement ids in the request's ids field are not valid.
    /// </summary>
    InvalidId,
    /// <summary>
    /// The entitlement ids in the request's ids field were not found.
    /// </summary>
    NotFound,
    /// <summary>
    /// The status of the entitlements in the request's ids field were successfully updated.
    /// </summary>
    Success,
    /// <summary>
    /// The user or organization identified by the request's user access token is not authorized to update the entitlements.
    /// </summary>
    Unauthorized,
    /// <summary>
    /// The update failed. These are considered transient errors and the request should be retried later.
    /// </summary>
    UpdateFailed
}
