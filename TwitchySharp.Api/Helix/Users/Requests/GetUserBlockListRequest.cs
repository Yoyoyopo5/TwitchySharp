using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets the list of users that the broadcaster has blocked.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-block-list">get user block list</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadBlockedUsers"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadBlockedUsers"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster to get blocked users for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value. 
/// </param>
public class GetUserBlockListRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetUserBlockListResponse>(
        "/users/blocks" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
