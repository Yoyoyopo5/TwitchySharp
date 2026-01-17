using TwitchySharp.Api.Models.Helix.Moderation.Models;

namespace TwitchySharp.Api.Models.Helix.Moderation.Responses;
/// <summary>
/// Contains a list of AutoMod settings.
/// </summary>
public record GetAutoModSettingsResponse
{
    /// <summary>
    /// Contains a single entry of a channel's current AutoMod settings.
    /// </summary>
    public required AutoModSettings[] Data { get; init; }
}
