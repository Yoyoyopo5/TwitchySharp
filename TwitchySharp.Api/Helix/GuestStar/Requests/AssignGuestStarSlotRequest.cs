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
/// Allows a previously invited user to be assigned a slot within the active Guest Star session, once that guest has indicated they are ready to join.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#assign-guest-star-slot">assign Guest Star slot</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster who is hosting the Guest Star session.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="sessionId">The id of the Guest Star session in which to assign the slot.</param>
/// <param name="guestId">
/// The user id of the guest to assign to the slot.
/// This user must have an invite to the session and have indicated that they are ready to join.
/// </param>
/// <param name="slotId">
/// The slot assignment to give to the user. 
/// Must be a numeric identifier between <c>"1"</c> and <c>"N"</c> where <c>N</c> is the max number of slots for the session. 
/// The max number of slots allowed for the session is reported by a <see cref="GetChannelGuestStarSettingsResponse"/>.
/// </param>
public class AssignGuestStarSlotRequest(
    string clientId, 
    string accessToken, 
    string broadcasterId, 
    string moderatorId, 
    string sessionId, 
    string guestId, 
    string slotId
    )
    : HelixApiRequest<AssignGuestStarSlotResponse>(
        "/guest_star/slot" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("session_id", sessionId)
            .Add("guest_id", guestId)
            .Add("slot_id", slotId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
