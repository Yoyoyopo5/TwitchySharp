using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets a list of unban requests for a broadcaster’s channel.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-unban-requests">Get Unban Requests</see> for more information.
/// </remarks>
public record GetUnbanRequestsRequest
    : TwitchHelixRequest<GetUnbanRequestsResponse>
{
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
    public GetUnbanRequestsRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        UnbanRequestStatus status,
        string? userId = null,
        string? after = null,
        int? first = null
        ) : base(
            "/moderation/unban_requests",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
                .Add("status", status.ToString().ToLowerInvariant())
                .Add("user_id", userId)
                .Add("after", after)
                .Add("first", first?.ToString())
            )
    {
        Method = HttpMethod.Get;
    }
}
