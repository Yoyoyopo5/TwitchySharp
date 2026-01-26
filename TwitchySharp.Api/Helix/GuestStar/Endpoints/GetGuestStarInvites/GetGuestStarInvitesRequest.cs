using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Provides the caller with a list of pending invites to a Guest Star session, including the invitee's ready status while joining the waiting room.
/// </summary>
/// <remarks>
/// Requires a user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-guest-star-invites">Get Guest Star Invites</see> for more information.
/// </remarks>
public record GetGuestStarInvitesRequest
    : TwitchHelixRequest<GetGuestStarInvitesResponse>
{
    protected override string Path => "/guest_star/invites";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ChannelReadGuestStar, Scope.ChannelManageGuestStar, Scope.ModeratorReadGuestStar, Scope.ModeratorManageGuestStar ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("session_id", SessionId);

    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session to get invites for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster or a moderator in the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user who created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// The session id to query for invites.
    /// </summary>
    public required GuestStarSessionId SessionId { get; set; }
}