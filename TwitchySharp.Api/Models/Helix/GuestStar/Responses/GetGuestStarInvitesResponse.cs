using TwitchySharp.Api.Models.Helix.GuestStar.Models;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Responses;
/// <summary>
/// Contains a list of invites for a specific Guest Star session.
/// </summary>
public record GetGuestStarInvitesResponse
{
    /// <summary>
    /// The list of invites.
    /// </summary>
    public required GuestStarInvite[] Data { get; init; }
}