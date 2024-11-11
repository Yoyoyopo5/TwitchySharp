using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using System.Collections;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Retrieves emotes available to the user across all channels.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-emotes">get user emotes</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.UserReadEmotes"/> and the access token must belong to the user you are requesting emotes for.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadEmotes"/>.</param>
/// <param name="userId">The user id of the user you want to get emotes for. This much be the same user that created the <paramref name="accessToken"/>.</param>
/// <param name="broadcasterId">
/// The user id of a broadcaster you wish to get follower emotes of. 
/// Using this query parameter will guarantee inclusion of the broadcaster’s follower emotes in the response body.
/// <b>Note:</b> If the user specified in <paramref name="userId"/> is subscribed to the broadcaster specified, their follower emotes will appear in the response body regardless if this query parameter is used.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="GetUserEmotesResponse.Pagination"/> in the response contains the cursor’s value.
/// </param>
public class GetUserEmotesRequest(string clientId, string accessToken, string userId, string? broadcasterId = null, string? after = null)
    : HelixApiRequest<GetUserEmotesResponse>(
        "/chat/emotes/user" +
        new HttpQueryParameters()
            .Add("user_id", userId)
            .Add("broadcaster_id", broadcasterId)
            .Add("after", after),
        clientId,
        accessToken
        );
