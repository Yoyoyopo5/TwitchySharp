using System.Net.Http;

namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Updates the Drop entitlement’s fulfillment status.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-drops-entitlements">Update Drops Entitlements</see> for more information.
/// </remarks>
public record UpdateDropsEntitlementsRequest
    : TwitchHelixRequest<UpdateDropsEntitlementsResponse>
{
    /// <param name="clientId">The client id of the application. This must be the same application that owns the game to update entitlements for.</param>
    /// <param name="accessToken">An app or user access token. If a user access token is used, only entitlements owned by the user that created it can be updated.</param>
    /// <param name="updates">The updates to make.</param>
    public UpdateDropsEntitlementsRequest(
        string clientId,
        string accessToken,
        UpdateDropsEntitlementsRequestData updates
        )
        : base(
            "/entitlements/drops",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = updates;
    }
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
