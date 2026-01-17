using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.GuestStar.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Requests;
/// <summary>
/// <b>BETA</b> Allows a caller to remove a slot assignment from a user participating in an active Guest Star session. 
/// </summary>
/// <remarks>
/// This revokes their access to the session immediately and disables their access to publish or subscribe to media within the session.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-guest-star-slot">Delete Guest Star Slot</see> for more information.
/// </remarks>
public record DeleteGuestStarSlotRequest
    : TwitchHelixRequest<DeleteGuestStarSlotResponse>
{
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
    public DeleteGuestStarSlotRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string guestId,
        string slotId,
        bool? shouldReinviteGuest = null
        ) : base(
            "/guest_star/slot",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
                .Add("session_id", sessionId)
                .Add("guest_id", guestId)
                .Add("slot_id", slotId)
                .Add("should_reinvite_guest", shouldReinviteGuest?.ToString())
            )
    {
        Method = HttpMethod.Delete;
    }
}
