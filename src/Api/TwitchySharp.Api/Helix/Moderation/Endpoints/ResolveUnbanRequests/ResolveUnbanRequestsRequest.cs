using System.Collections.Immutable;
using System.Net.Http;
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
    protected override string Path => "/moderation/unban_requests";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageUnbanRequests)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("unban_request_id", UnbanRequestId)
            .Add("status", Status.Value)
            .Add("resolution_text", ResolutionText);

    /// <summary>
    /// The user id of the broadcaster (channel) to resolve the unban request for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The id of the unban request to resolve.
    /// </summary>
    public required UnbanRequestId UnbanRequestId { get; init; }

    /// <summary>
    /// The resolution status to set the unban request to.
    /// </summary>
    public required UnbanRequestResolutionStatus Status { get; init; }

    /// <summary>
    /// Caller-defined text that is added to the unban request.
    /// </summary>
    /// <remarks>
    /// This can be a maximum of 500 characters.
    /// </remarks>
    public string? ResolutionText { get; init; }
}
