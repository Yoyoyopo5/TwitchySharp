using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.GuestStar.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Requests;
/// <summary>
/// <b>BETA</b> Allows a user to update the assigned slot for a particular user within the active Guest Star session.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-guest-star-slot">Update Guest Star Slot</see> for more information.
/// </remarks>
public record UpdateGuestStarSlotRequest
    : TwitchHelixRequest<UpdateGuestStarSlotResponse>
{
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
    public UpdateGuestStarSlotRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string sourceSlotId,
        string? destinationSlotId = null
        ) : base(
            "/guest_star/slot",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
                .Add("session_id", sessionId)
                .Add("source_slot_id", sourceSlotId)
                .Add("destination_slot_id", destinationSlotId)
            )
    {
        Method = HttpMethod.Patch;
    }
}
