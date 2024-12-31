using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Resolves an unban request by approving or denying it.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#resolve-unban-requests">resolve unban requests</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageUnbanRequests"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageUnbanRequests"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to resolve the unban request for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="unbanRequestId">The id of the unban request to resolve.</param>
/// <param name="status">The resolution status to set the unban request to.</param>
/// <param name="resolutionText">
/// Caller-defined text that is added to the unban request. 
/// This can be a maximum of 500 characters.
/// </param>
public class ResolveUnbanRequestsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    string unbanRequestId,
    UnbanRequestResolutionStatus status,
    string? resolutionText = null
    )
    : HelixApiRequest<ResolveUnbanRequestsResponse>(
        "/moderation/unban_requests" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("unban_request_id", unbanRequestId)
            .Add("status", status.ToString().ToLowerInvariant())
            .Add("resolution_text", resolutionText),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}
