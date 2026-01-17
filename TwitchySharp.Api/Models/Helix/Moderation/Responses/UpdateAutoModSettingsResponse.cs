using TwitchySharp.Api.Models.Helix.Moderation.Models;

namespace TwitchySharp.Api.Models.Helix.Moderation.Responses;
/// <summary>
/// Contains the list of updated AutoMod settings.
/// </summary>
public record UpdateAutoModSettingsResponse
{
    /// <summary>
    /// A list with a single entry of the channel's updated AutoMod settings.
    /// </summary>
    public required AutoModSettings[] Data { get; init; }
}
