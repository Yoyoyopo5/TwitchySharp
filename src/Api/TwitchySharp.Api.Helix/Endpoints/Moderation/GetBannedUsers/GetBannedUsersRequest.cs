using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets all users that the broadcaster banned or put in a timeout.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.ModerationRead"/> or <see cref="Scope.ModeratorManageBannedUsers"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModerationRead"/> or <see cref="Scope.ModeratorManageBannedUsers"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-banned-users">Get Banned Users</see> for more information.
/// </remarks>
public record GetBannedUsersRequest
    : TwitchHelixRequest<GetBannedUsersResponseContent>, IForwardPageableRequest, IBackwardPageableRequest,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/banned";
    public override HttpMethod Method => HttpMethod.Get;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModerationRead, Scope.ModeratorManageBannedUsers)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserIds?.Select(x => x.Value))
            .Add("first", First?.ToString())
            .Add("after", After?.Value)
            .Add("before", Before?.Value);

    /// <summary>
    /// The user id of the broadcaster (channel) to get banned users for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// A list of user ids used to filter the results.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 IDs.
    /// The returned list includes only those users that were banned or put in a timeout.
    /// The list is returned in the same order that you specified the ids.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; init; }

    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? Before { get; init; }
}
