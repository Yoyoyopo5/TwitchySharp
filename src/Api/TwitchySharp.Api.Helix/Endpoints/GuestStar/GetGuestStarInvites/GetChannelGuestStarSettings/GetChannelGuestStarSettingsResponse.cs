namespace TwitchySharp.Api.Helix.GuestStar;
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
