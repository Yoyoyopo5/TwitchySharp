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
/// Allows a caller to remove a slot assignment from a user participating in an active Guest Star session. 
/// This revokes their access to the session immediately and disables their access to publish or subscribe to media within the session.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-guest-star-slot">delete Guest Star slot</see> for more information.
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
/// <param name="sessionId">The id of the Guest Star session from which to remove a user.</param>
/// <param name="guestId">The user id of the user to remove from the Guest Star session.</param>
/// <param name="slotId">The id of the slot from which to remove the user from.</param>
/// <param name="shouldReinviteGuest">Determines whether the user should be reinvited to the session, sending them back to the invite queue.</param>
public class DeleteGuestStarSlotRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    string sessionId,
    string guestId,
    string slotId,
    bool? shouldReinviteGuest = null
    )
    : HelixApiRequest<DeleteGuestStarSlotResponse>(
        "/guest_star/slot" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("session_id", sessionId)
            .Add("guest_id", guestId)
            .Add("slot_id", slotId)
            .Add("should_reinvite_guest", shouldReinviteGuest?.ToString()),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
