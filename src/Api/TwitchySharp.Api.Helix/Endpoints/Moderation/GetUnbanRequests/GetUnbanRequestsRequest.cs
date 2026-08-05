using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets a list of unban requests for a broadcaster's channel.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadUnbanRequests"/> or <see cref="Scope.ModeratorManageUnbanRequests"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-unban-requests">Get Unban Requests</see> for more information.
/// </remarks>
public record GetUnbanRequestsRequest
    : TwitchHelixRequest<GetUnbanRequestsResponse>, IForwardPageableRequest
{
    protected override string Path => "/moderation/unban_requests";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorReadUnbanRequests, Scope.ModeratorManageUnbanRequests)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("status", Status.Value)
            .Add("user_id", UserId)
            .Add("after", After?.Value)
            .Add("first", First?.ToString());

    /// <summary>
    /// The user id of the broadcaster (channel) to get unban requests for.
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
    /// Filter unban requests by status.
    /// </summary>
    public required UnbanRequestStatus Status { get; init; }

    /// <summary>
    /// Filter unban requests by banned user.
    /// </summary>
    public UserId? UserId { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <inheritdoc cref="PaginationAmount"/>
    public PaginationAmount? First { get; init; }
}
