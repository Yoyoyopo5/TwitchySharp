using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes the ban or timeout that was placed on the specified user.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageBannedUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#unban-user">Unban User</see> for more information.
/// </remarks>
public record UnbanUserRequest
    : TwitchHelixRequest<UnbanUserResponse>
{
    protected override string Path => "/moderation/bans";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageBannedUsers)
    };
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

    protected override ValueTask<UnbanUserResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new UnbanUserResponse());
}
