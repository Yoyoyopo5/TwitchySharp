using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Sends an invite to a specified guest on behalf of the broadcaster for a Guest Star session in progress.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-guest-star-invite">Send Guest Star Invite</see> for more information.
/// </remarks>
public record SendGuestStarInviteRequest
    : TwitchHelixRequest<SendGuestStarInviteResponse>
{
    protected override string Path => "/guest_star/invites";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelManageGuestStar, Scope.ModeratorManageGuestStar);
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
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The id of the Guest Star session that you want to send an invite to.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }

    /// <summary>
    /// The user id of the user to send the invite to.
    /// </summary>
    public required UserId GuestId { get; init; }
}