using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets the list of users that the broadcaster has blocked.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadBlockedUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-block-list">Get User Block List</see> for more information.
/// </remarks>
public record GetUserBlockListRequest
    : TwitchHelixRequest<GetUserBlockListResponse>, IForwardPageableRequest
{
    protected override string Path => "/users/blocks";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserReadBlockedUsers)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster to get blocked users for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }
    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
