using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
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
    /// <param name="parameters">The request parameters.</param>
    public UpdateGuestStarSlotRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        UpdateGuestStarSlotRequestParameters parameters
        ) : base(
            "/guest_star/slot",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("moderator_id", parameters.ModeratorId)
                .Add("session_id", parameters.SessionId)
                .Add("source_slot_id", parameters.SourceSlotId)
                .Add("destination_slot_id", parameters.DestinationSlotId)
            )
    {
        Method = HttpMethod.Patch;
    }
}

/// <summary>
/// Request parameters for a <see cref="UpdateGuestStarSlotRequest"/>.
/// </summary>
public record UpdateGuestStarSlotRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// The id of the Guest Star session in which to update a slot.
    /// </summary>
    public required GuestStarSessionId SessionId { get; set; }

    /// <summary>
    /// The id of the slot containing the user you want to move.
    /// </summary>
    public required GuestStarSlotId SourceSlotId { get; set; }

    /// <summary>
    /// The id of the slot to move the source user to.
    /// </summary>
    /// <remarks>
    /// If the destination slot is occupied, the user assigned will be swapped into the source slot.
    /// </remarks>
    public GuestStarSlotId? DestinationSlotId { get; set; }
}
