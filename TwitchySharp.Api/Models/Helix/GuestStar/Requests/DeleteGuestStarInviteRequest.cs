using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.GuestStar.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Requests;
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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
    /// <param name="broadcasterId">The user id of the broadcaster hosting the Guest Star session.</param>
    /// <param name="moderatorId">
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="sessionId">The id of the Guest Star session that the invite was created for.</param>
    /// <param name="guestId">The user id of the user to revoke the invite for.</param>
    public DeleteGuestStarInviteRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        string moderatorId,
        string sessionId,
        string guestId
        ) : base(
            "/guest_star/invites",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("moderator_id", moderatorId)
                .Add("session_id", sessionId)
                .Add("guest_id", guestId)
            )
    {
        Method = HttpMethod.Delete;
    }
}
