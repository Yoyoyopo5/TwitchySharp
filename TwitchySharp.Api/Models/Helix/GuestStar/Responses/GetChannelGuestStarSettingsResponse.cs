using System.Text.Json.Serialization;
using TwitchySharp.Api.Models.Helix.GuestStar.Models;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Responses;
/// <summary>
/// Contains a list containing the channel's Guest Star settings.
/// </summary>
public record GetChannelGuestStarSettingsResponse
{
    /// <summary>
    /// Contains a single entry of the channel's Guest Star settings.
    /// </summary>
    public required ChannelGuestStarSettings[] Data { get; init; }
}
