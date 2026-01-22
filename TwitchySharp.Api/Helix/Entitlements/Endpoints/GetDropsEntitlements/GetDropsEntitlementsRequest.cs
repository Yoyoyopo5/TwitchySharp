using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Gets an organization’s list of entitlements that have been granted to a game, a user, or both.
/// </summary>
/// <remarks>
/// <para>
/// <b>Note:</b> Entitlements returned in the response body data are not guaranteed to be sorted by any field returned by the API. 
/// To retrieve <see cref="DropsEntitlementStatus.Claimed"/> or <see cref="DropsEntitlementStatus.Fulfilled"/> entitlements, 
/// use the <paramref name="fulfillmentStatus"/> parameter to filter results. 
/// To retrieve entitlements for a specific game, use the <paramref name="gameId"/> query parameter to filter results.
/// Parameter use varies based on the type of token used.
/// </para>
/// Requires an app or user access token. 
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-drops-entitlements">Get Drops Entitlements</see> for more information.
/// </remarks>
public record GetDropsEntitlementsRequest
    : TwitchHelixRequest<GetDropsEntitlementsResponse>
{
    /// <param name="clientId">The client id of the application. This application must be the owner of the game to get entitlements for.</param>
    /// <param name="accessToken">
    /// An app or user access token. 
    /// If you use a user access token, you will only get entitlements for the user that created it.
    /// </param>
    /// <param name="parameters">The request parameters.</param>
    public GetDropsEntitlementsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetDropsEntitlementsRequestParameters? parameters = null
        )
        : base(
            "/entitlements/drops",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", parameters?.Ids?.Select(x => x.ToString()))
                .Add("user_id", parameters?.UserId)
                .Add("game_id", parameters?.GameId)
                .Add("fulfillment_status", parameters?.FulfillmentStatus?.Value)
                .Add("after", parameters?.After?.Value)
                .Add("first", parameters?.First?.ToString())
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetDropsEntitlementsRequest"/>.
/// </summary>
public record GetDropsEntitlementsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The ids of the specific entitlements to get.
    /// </summary>
    public IEnumerable<DropsEntitlementId>? Ids { get; set; }
    /// <summary>
    /// The user id to get entitlements for.
    /// </summary>
    /// <remarks>
    /// Use this parameter to get all entitlements for a specific user.
    /// You can combine this parameter with <see cref="GameId"/>.
    /// Requires the use of an app access token for the request.
    /// </remarks>
    public UserId? UserId { get; set; }
    /// <summary>
    /// The game id to get entitlements for.
    /// </summary>
    /// <remarks>
    /// Use this parameter to get all entitlements for a specific game.
    /// You can combine this parameter with <see cref="UserId"/> if using an app access token.
    /// </remarks>
    public GameId? GameId { get; set; }
    /// <summary>
    /// Filters the returned entitlements by a specified fulfillment status.
    /// </summary>
    public DropsEntitlementStatus? FulfillmentStatus { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 entitlement per page and the maximum is 1000. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
