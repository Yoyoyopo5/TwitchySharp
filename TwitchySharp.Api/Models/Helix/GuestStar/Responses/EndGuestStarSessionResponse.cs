using TwitchySharp.Api.Models.Helix.GuestStar.Models;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Responses;
/// <summary>
/// Contains details about the session that was ended.
/// </summary>
public record EndGuestStarSessionResponse
{
    /// <summary>
    /// Contains a single entry of a summary of the session details when the session was ended.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}
