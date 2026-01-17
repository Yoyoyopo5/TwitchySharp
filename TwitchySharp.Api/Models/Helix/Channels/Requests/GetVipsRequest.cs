using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Channels.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Channels.Requests;
/// <summary>
/// Gets a list of the broadcaster’s VIPs.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-vips">Get VIPs</see> for more information.
/// </remarks>
public record GetVipsRequest
    : TwitchHelixRequest<GetVipsResponse>
{
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
    public GetVipsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        IEnumerable<string>? userIds = null,
        int? first = null,
        string? after = null
        )
        : base(
            "/channels/vips",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("user_id", userIds)
                .Add("first", first?.ToString())
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
