using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Resolves an unban request by approving or denying it.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageUnbanRequests"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#resolve-unban-requests">Resolve Unban Requests</see> for more information.
/// </remarks>
public record ResolveUnbanRequestsRequest
    : TwitchHelixRequest<ResolveUnbanRequestsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageUnbanRequests"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public ResolveUnbanRequestsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        ResolveUnbanRequestsRequestParameters parameters
        ) : base(
            "/moderation/unban_requests",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("unban_request_id", parameters.UnbanRequestId)
                .Add("status", parameters.Status.Value)
                .Add("resolution_text", parameters.ResolutionText)
            )
    {
        Method = HttpMethod.Patch;
    }
}

/// <summary>
/// Request parameters for a <see cref="ResolveUnbanRequestsRequest"/>.
/// </summary>
public record ResolveUnbanRequestsRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster (channel) to resolve the unban request for.
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
    /// The id of the unban request to resolve.
    /// </summary>
    public required UnbanRequestId UnbanRequestId { get; set; }
    /// <summary>
    /// The resolution status to set the unban request to.
    /// </summary>
    public required UnbanRequestResolutionStatus Status { get; set; }
    /// <summary>
    /// Caller-defined text that is added to the unban request.
    /// </summary>
    /// <remarks>
    /// This can be a maximum of 500 characters.
    /// </remarks>
    public string? ResolutionText { get; set; }
}
