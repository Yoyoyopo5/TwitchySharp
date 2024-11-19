using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Updates the Drop entitlement’s fulfillment status.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-drops-entitlements">update drops entitlements</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application. This must be the same application that owns the game to update entitlements for.</param>
/// <param name="accessToken">An app or user access token. If a user access token is used, only entitlements owned by the user that created it can be updated.</param>
/// <param name="updates">The updates to make.</param>
public class UpdateDropsEntitlementsRequest(string clientId, string accessToken, UpdateDropsEntitlementsRequestData updates)
    : HelixApiRequest<UpdateDropsEntitlementsResponse, UpdateDropsEntitlementsRequestData>(
        "/entitlements/drops",
        clientId,
        accessToken,
        updates
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Contains information used to request an update to a set of drops entitlements.
/// </summary>
public record UpdateDropsEntitlementsRequestData
{
    /// <summary>
    /// The ids of the entitlements to update.
    /// </summary>
    public string[]? EntitlementIds { get; set; }
    /// <summary>
    /// The fulfillment status to update the entitlements to.
    /// </summary>
    public DropsEntitlementStatus? FulfillmentStatus { get; set; }
}
