using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Revokes a previously sent invite for a Guest Star session.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-guest-star-invite">Delete Guest Star Invite</see> for more information.
/// </remarks>
public record DeleteGuestStarInviteRequest
    : TwitchHelixRequest<DeleteGuestStarInviteResponse>
{
    protected override string Path => "/guest_star/invites";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageGuestStar, Scope.ModeratorManageGuestStar)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("session_id", SessionId)
            .Add("guest_id", GuestId);

    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The id of the Guest Star session that the invite was created for.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }

    /// <summary>
    /// The user id of the user to revoke the invite for.
    /// </summary>
    public required UserId GuestId { get; init; }

    protected override ValueTask<DeleteGuestStarInviteResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new DeleteGuestStarInviteResponse());
}
