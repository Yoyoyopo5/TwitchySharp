using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Entitlements;
/// <summary>
/// Gets an organization’s list of entitlements that have been granted to a game, a user, or both.
/// <br/>
/// <b>Note:</b> Entitlements returned in the response body data are not guaranteed to be sorted by any field returned by the API. 
/// To retrieve <see cref="DropsEntitlementStatus.Claimed"/> or <see cref="DropsEntitlementStatus.Fulfilled"/> entitlements, 
/// use the <paramref name="fulfillmentStatus"/> parameter to filter results. 
/// To retrieve entitlements for a specific game, use the <paramref name="gameId"/> query parameter to filter results.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-drops-entitlements">get drops entitlements</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token. 
/// Parameter use varies based on the type of token used.
/// </remarks>
/// <param name="clientId">The client id of the application. This application must be the owner of the game to get entitlements for.</param>
/// <param name="accessToken">
/// An app or user access token. 
/// If you use a user access token, you will only get entitlements for the user that created it.
/// </param>
/// <param name="id">
/// The ids of the specific entitlements to get. 
/// Use this parameter to get specific entitlements.
/// </param>
/// <param name="userId">
/// The user id to get entitlements for.
/// Use this parameter to get all entitlements for a specific user.
/// You can combine this parameter with <paramref name="gameId"/>.
/// Requires the use of an app access token for <paramref name="accessToken"/>.
/// </param>
/// <param name="gameId">
/// The game id to get entitlements for.
/// Use this parameter to get all entitlements for a specific game.
/// You can combine this parameter with <paramref name="userId"/> if using an app access token.
/// </param>
/// <param name="fulfillmentStatus">
/// Filters the returned entitlements by a specified fulfillment status.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="GetDropsEntitlementsResponse.Pagination"/> in the response contains the cursor’s value.
/// </param>
/// <param name="first">
/// The maximum number of entitlements to return per page in the response. 
/// The minimum page size is 1 entitlement per page and the maximum is 1000. 
/// The default is 20.
/// </param>
public class GetDropsEntitlementsRequest(
    string clientId, 
    string accessToken, 
    IEnumerable<string>? id = null,
    string? userId = null,
    string? gameId = null,
    DropsEntitlementStatus? fulfillmentStatus = null,
    string? after = null,
    int? first = null
    )
    : HelixApiRequest<GetDropsEntitlementsResponse>(
        "/entitlements/drops" +
        new HttpQueryParameters()
            .Add("id", id)
            .Add("user_id", userId)
            .Add("game_id", gameId)
            .Add("fulfillment_status", fulfillmentStatus?.ToString().ToUpper())
            .Add("after", after)
            .Add("first", first?.ToString()),
        clientId,
        accessToken
        );
