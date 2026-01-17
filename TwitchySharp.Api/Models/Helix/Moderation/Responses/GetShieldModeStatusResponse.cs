using TwitchySharp.Api.Models.Helix.Moderation.Models;

namespace TwitchySharp.Api.Models.Helix.Moderation.Responses;
/// <summary>
/// Contains information about a channel's current Shield Mode status.
/// </summary>
public record GetShieldModeStatusResponse
{
    /// <summary>
    /// A list containing a single entry of a channel's Shield Mode status.
    /// </summary>
    public required ShieldModeStatus[] Data { get; init; }
}
