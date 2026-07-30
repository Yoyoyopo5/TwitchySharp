namespace TwitchySharp.Api.Helix.GuestStar;
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
