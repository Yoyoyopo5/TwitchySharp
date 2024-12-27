using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// BETA
/// <br/>
/// Sends an invite to a specified guest on behalf of the broadcaster for a Guest Star session in progress.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-guest-star-invite">send Guest Star invite</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster hosting the Guest Star session.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="sessionId">The id of the Guest Star session that you want to send an invite to.</param>
/// <param name="guestId">The user id of the user to send the invite to.</param>
public class SendGuestStarInviteRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string moderatorId, 
    string sessionId, 
    string guestId
    ) 
    : HelixApiRequest<SendGuestStarInviteResponse>(
        "/guest_star/invites" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("session_id", sessionId)
            .Add("guest_id", guestId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
