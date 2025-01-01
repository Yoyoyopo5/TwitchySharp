using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets a list of the broadcaster’s VIPs.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-vips">get VIPs</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to get VIPs for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="userIds">
/// Filter the list by specific users.
/// The maximum number of ids that you may specify is 100. 
/// Ignores the ids of users that aren’t VIPs on the broadcaster's channel.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// </param>
public class GetVipsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    IEnumerable<string>? userIds = null,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetVipsResponse>(
        "/channels/vips" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("user_id", userIds)
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
