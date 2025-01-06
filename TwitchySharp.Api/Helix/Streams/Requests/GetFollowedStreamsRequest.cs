using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets the list of broadcasters that the user follows and that are streaming live.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-followed-streams">get followed streams</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadFollows"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadFollows"/>.</param>
/// <param name="userId">
/// The id of the user to get followed streams for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 100.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
public class GetFollowedStreamsRequest(
    string clientId,
    string accessToken,
    string userId,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetFollowedStreamsResponse>(
        "/streams/followed" +
        new HttpQueryParameters()
            .Add("user_id", userId)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
