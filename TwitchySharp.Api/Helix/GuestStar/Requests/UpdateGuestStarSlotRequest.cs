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
/// Allows a user to update the assigned slot for a particular user within the active Guest Star session.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-guest-star-slot">update Guest Star slot</see> for more information.
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
/// <param name="sessionId">The id of the Guest Star session in which to update a slot.</param>
/// <param name="sourceSlotId">The id of the slot containing the user you want to move.</param>
/// <param name="destinationSlotId">
/// The id of the slot to move the <paramref name="sourceSlotId"/> user to.
/// If the destination slot is occupied, the user assigned will be swapped into <paramref name="sourceSlotId"/>.
/// </param>
public class UpdateGuestStarSlotRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    string sessionId,
    string sourceSlotId,
    string? destinationSlotId = null
    )
    : HelixApiRequest<UpdateGuestStarSlotResponse>(
        "/guest_star/slot" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId)
            .Add("session_id", sessionId)
            .Add("source_slot_id", sourceSlotId)
            .Add("destination_slot_id", destinationSlotId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}
