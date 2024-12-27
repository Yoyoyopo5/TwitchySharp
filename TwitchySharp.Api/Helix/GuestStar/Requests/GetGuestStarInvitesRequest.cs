using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// BETA
/// <br/>
/// Provides the caller with a list of pending invites to a Guest Star session, including the invitee’s ready status while joining the waiting room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-guest-star-invites">get Guest Star invites</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster hosting the Guest Star session to get invites for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator in the broadcaster's channel.
/// This must be the same user who created the <paramref name="accessToken"/>.
/// </param>
/// <param name="sessionId">The session id to query for invites.</param>
public class GetGuestStarInvitesRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string moderatorId, 
    string sessionId
    )
    : HelixApiRequest<GetGuestStarInvitesResponse>(
        "/guest_star/invites" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("session_id", sessionId),
        clientId,
        accessToken
        );
