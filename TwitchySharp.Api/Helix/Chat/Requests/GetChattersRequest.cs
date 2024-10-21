using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the list of users that are connected to the broadcaster’s chat session.
/// <br/>
/// <b>NOTE:</b> There is a delay between when users join and leave a chat and when the list is updated accordingly.
/// <br/>
/// <b>DEV NOTE:</b> The list is usually not very accurate (in real-time) for this reason. 
/// Sometimes a user will not be in this list when they are active in chat.
/// <br/>
/// To determine whether a user is a moderator or VIP, use the Get Moderators and Get VIPs endpoints. 
/// You can check the roles of up to 100 users.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-chatters">get chatters</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadChatters"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.ModeratorReadChatters"/></param>
/// <param name="broadcasterId">The user id of the broadcaster whose chatters you want to get.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster OR one of the broadcaster's moderators.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 1,000. 
/// The default is 100.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
public class GetChattersRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string moderatorId, 
    int? first = null, 
    string? after = null
    )
    : HelixApiRequest<GetChattersResponse>(
        "/chat/chatters" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
