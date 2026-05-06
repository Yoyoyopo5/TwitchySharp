using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Updates the Drop entitlement's fulfillment status.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-drops-entitlements">Update Drops Entitlements</see> for more information.
/// </remarks>
public record UpdateDropsEntitlementsRequest
    : TwitchHelixRequest<UpdateDropsEntitlementsResponse>
{
    protected override string Path => "/entitlements/drops";
    public override HttpMethod Method => HttpMethod.Patch;
    public override object? ContentObject => Updates;

    /// <summary>
    /// The updates to make to the entitlements.
    /// </summary>
    public required UpdateDropsEntitlementsRequestData Updates { get; init; }
}

/// <summary>
/// Contains information used to request an update to a set of drops entitlements.
/// </summary>
public record UpdateDropsEntitlementsRequestData
{
    /// <summary>
    /// The ids of the entitlements to update.
    /// </summary>
    public IEnumerable<DropsEntitlementId>? EntitlementIds { get; init; }
    /// <summary>
    /// The fulfillment status to update the entitlements to.
    /// </summary>
    public DropsEntitlementStatus? FulfillmentStatus { get; init; }
}
