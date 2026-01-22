using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Allows a previously invited user to be assigned a slot within the active Guest Star session, once that guest has indicated they are ready to join.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#assign-guest-star-slot">Assign Guest Star Slot</see> for more information.
/// </remarks>
public record AssignGuestStarSlotRequest
    : TwitchHelixRequest<AssignGuestStarSlotResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public AssignGuestStarSlotRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        AssignGuestStarSlotRequestParameters parameters
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
            )
    {
        Method = HttpMethod.Post;
    }
}

/// <summary>
/// Request parameters for a <see cref="AssignGuestStarSlotRequest"/>.
/// </summary>
public record AssignGuestStarSlotRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster who is hosting the Guest Star session.
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
    /// The id of the Guest Star session in which to assign the slot.
    /// </summary>
    public required GuestStarSessionId SessionId { get; set; }
    /// <summary>
    /// The user id of the guest to assign to the slot.
    /// </summary>
    /// <remarks>
    /// This user must have an invite to the session and have indicated that they are ready to join.
    /// </remarks>
    public required UserId GuestId { get; set; }
    /// <summary>
    /// The slot assignment to give to the user.
    /// </summary>
    /// <remarks>
    /// Must be a numeric identifier between <c>"1"</c> and <c>"N"</c> where <c>N</c> is the max number of slots for the session. 
    /// The max number of slots allowed for the session is reported by a <see cref="GetChannelGuestStarSettingsResponse"/>.
    /// </remarks>
    public required GuestStarSlotId SlotId { get; set; }
}