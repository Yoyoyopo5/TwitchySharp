using TwitchySharp.Api.Models.Helix.Moderation.Models;

namespace TwitchySharp.Api.Models.Helix.Moderation.Responses;
/// <summary>
/// Contains information about a channel's Shield Mode status.
/// </summary>
public record UpdateShieldModeStatusResponse
{
    /// <summary>
    /// A list containing a single object of the channel's Shield Mode status.
    /// </summary>
    public required ShieldModeStatus[] Data { get; init; }
}