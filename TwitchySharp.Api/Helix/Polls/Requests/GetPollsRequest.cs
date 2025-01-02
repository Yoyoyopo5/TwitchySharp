using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Gets a list of polls that the broadcaster created.
/// Polls are available for 90 days after they’re created.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-polls">get polls</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadPolls"/> or <see cref="Scope.ChannelManagePolls"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to get polls for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="pollIds">
/// Filter the list of polls by poll id.
/// You may specify a maximum of 20 ids.
/// Specify this parameter only if you want to filter the list that the request returns. 
/// The endpoint ignores duplicate ids and those not owned by this broadcaster.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 20 items per page. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// </param>
public class GetPollsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    IEnumerable<string>? pollIds,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetPollsResponse>(
        "/polls" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("id", pollIds)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
