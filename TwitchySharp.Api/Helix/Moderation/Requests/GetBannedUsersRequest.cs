using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets all users that the broadcaster banned or put in a timeout.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-banned-users">get banned users</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModerationRead"/> or <see cref="Scope.ModeratorManageBannedUsers"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModerationRead"/> or <see cref="Scope.ModeratorManageBannedUsers"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to get banned users for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="userIds">
/// A list of user ids used to filter the results. You may specify a maximum of 100 IDs. 
/// The returned list includes only those users that were banned or put in a timeout.
/// The list is returned in the same order that you specified the ids.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// </param>
/// <param name="before">
/// The cursor used to get the previous page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// </param>
public class GetBannedUsersRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    IEnumerable<string>? userIds = null,
    int? first = null,
    string? after = null,
    string? before = null
    )
    : HelixApiRequest<GetBannedUsersResponse>(
        "/moderation/banned" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("user_id", userIds)
            .Add("first", first?.ToString())
            .Add("after", after)
            .Add("before", before),
        clientId,
        accessToken
        );
