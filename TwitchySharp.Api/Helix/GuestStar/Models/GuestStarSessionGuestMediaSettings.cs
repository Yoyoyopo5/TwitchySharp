namespace TwitchySharp.Api.Helix.GuestStar;

/// <summary>
/// Contains information about a guest's media settings (whether audio or video).
/// </summary>
public record GuestStarSessionGuestMediaSettings
{
    /// <summary>
    /// Determines whether the host is allowing the guest’s audio/video to be seen or heard within the session.
    /// </summary>
    public required bool IsHostEnabled { get; init; }
    /// <summary>
    /// Determines whether the guest is allowing their audio/video to be transmitted to the session.
    /// </summary>
    public required bool IsGuestEnabled { get; init; }
    /// <summary>
    /// Determines whether the guest has an appropriate audio/video device available to be transmitted to the session.
    /// </summary>
    public required bool IsAvailable { get; init; }
}
