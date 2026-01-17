using TwitchySharp.Api.Models.Helix.GuestStar.Models;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Responses;
/// <summary>
/// Contains data about the newly created Guest Star session.
/// </summary>
public record CreateGuestStarSessionResponse
{
    /// <summary>
    /// A list containing a single entry for the newly created Guest Star session.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}
