namespace TwitchySharp.Api.Helix.GuestStar;

/// <summary>
/// Contains information about a channel's Guest Star settings.
/// </summary>
public record ChannelGuestStarSettings
{
    /// <summary>
    /// Determines if Guest Star moderators have access to control whether a guest is live once assigned to a slot.
    /// </summary>
    public required bool IsModeratorSendLiveEnabled { get; init; }
    /// <summary>
    /// Number of slots the Guest Star call interface will allow the host to add to a call. 
    /// Ranges between 1 and 6.
    /// </summary>
    public required int SlotCount { get; init; }
    /// <summary>
    /// Determines if Browser Sources subscribed to sessions on this channel should output audio.
    /// </summary>
    public required bool IsBrowserSourceAudioEnabled { get; init; }
    /// <summary>
    /// Determines how the guests within a session should be laid out within the browser source.
    /// </summary>
    public required GuestStarGroupLayout GroupLayout { get; init; }
    /// <summary>
    /// A token used to generate browser source URLs.
    /// </summary>
    public required string BrowserSourceToken { get; init; }
}
