using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
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
    /// <param name="parameters">The request parameters.</param>
    public DeleteGuestStarSlotRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        DeleteGuestStarSlotRequestParameters parameters
        ) : base(
            "/guest_star/slot",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("session_id", parameters.SessionId)
                .Add("guest_id", parameters.GuestId)
                .Add("slot_id", parameters.SlotId)
                .Add("should_reinvite_guest", parameters.ShouldReinviteGuest?.ToString())
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="DeleteGuestStarInviteRequest"/>.
/// </summary>
public record DeleteGuestStarSlotRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }
    /// <summary>
    /// The id of the Guest Star session from which to remove a user.
    /// </summary>
    public required GuestStarSessionId SessionId { get; set; }
    /// <summary>
    /// The user id of the user to remove from the Guest Star session.
    /// </summary>
    public required UserId GuestId { get; set; }
    /// <summary>
    /// The id of the slot from which to remove the user from.
    /// </summary>
    public required GuestStarSlotId SlotId { get; set; }
    /// <summary>
    /// Determines whether the user should be reinvited to the session, sending them back to the invite queue.
    /// </summary>
    public bool? ShouldReinviteGuest { get; set; }
}
