using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes the ban or timeout that was placed on the specified user.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageBannedUsers"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageBannedUsers"/> and <see cref="Scope.UserBot"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#unban-user">Unban User</see> for more information.
/// </remarks>
public record UnbanUserRequest
    : TwitchHelixRequest<UnbanUserResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/bans";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageBannedUsers)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster (channel) that the user will be unbanned on.
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
    /// The user id of the user to unban or remove a time-out on.
    /// </summary>
    public required UserId UserId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<UnbanUserResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new UnbanUserResponseContent());
}
