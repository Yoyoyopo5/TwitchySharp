using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public GetUnbanRequestsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetUnbanRequestsRequestParameters parameters
        ) : base(
            "/moderation/unban_requests",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("status", parameters.Status.Value)
                .Add("user_id", parameters.UserId)
                .Add("after", parameters.After?.Value)
                .Add("first", parameters.First?.ToString())
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetUnbanRequestsRequest"/>.
/// </summary>
public record GetUnbanRequestsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get unban requests for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
    /// <summary>
    /// Filter unban requests by status.
    /// </summary>
    public required UnbanRequestStatus Status { get; set; }
    /// <summary>
    /// Filter unban requests by banned user.
    /// </summary>
    public UserId? UserId { get; set; }
    public PaginationCursor? After { get; set; }
    public PaginationAmount? First { get; set; }
}
