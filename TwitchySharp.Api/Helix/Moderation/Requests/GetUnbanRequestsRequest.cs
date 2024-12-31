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
/// Gets a list of unban requests for a broadcaster’s channel.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-unban-requests">get unban requests</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to get unban requests for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="status">Filter unban requests by status.</param>
/// <param name="userId">Filter unban requests by banned user.</param>
/// <param name="after">
/// Cursor used to get next page of results. 
/// The <see cref="Pagination"/> property in response contains the cursor value.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response.
/// </param>
public class GetUnbanRequestsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    UnbanRequestStatus status,
    string? userId = null,
    string? after = null,
    int? first = null
    )
    : HelixApiRequest<GetUnbanRequestsResponse>(
        "/moderation/unban_requests" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("status", status.ToString().ToLowerInvariant())
            .Add("user_id", userId)
            .Add("after", after)
            .Add("first", first?.ToString()),
        clientId,
        accessToken
        );
