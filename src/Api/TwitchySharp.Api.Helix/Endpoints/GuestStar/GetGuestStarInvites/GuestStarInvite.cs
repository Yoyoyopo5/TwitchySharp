namespace TwitchySharp.Api.Helix.GuestStar;

/// <summary>
/// Contains data about a specific Guest Star invite.
/// </summary>
public record GuestStarInvite
{
    /// <summary>
    /// The user id of the invited guest.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The time when this invite was created.
    /// </summary>
    public required DateTimeOffset InvitedAt { get; init; }
    /// <summary>
    /// Status representing the invited user’s join state.
    /// </summary>
    public required GuestStarInviteStatus Status { get; init; }
    /// <summary>
    /// Flag signaling that the invited user has chosen to disable their local video device. 
    /// The user has hidden themselves, but they may choose to reveal their video feed upon joining the session.
    /// </summary>
    public required bool IsVideoEnabled { get; init; }
    /// <summary>
    /// Flag signaling that the invited user has chosen to disable their local audio device. 
    /// The user has muted themselves, but they may choose to unmute their audio feed upon joining the session.
    /// </summary>
    public required bool IsAudioEnabled { get; init; }
    /// <summary>
    /// Flag signaling that the invited user has a video device available for sharing.
    /// </summary>
    public required bool IsVideoAvailable { get; init; }
    /// <summary>
    /// Flag signaling that the invited user has an audio device available for sharing.
    /// </summary>
    public required bool IsAudioAvailable { get; init; }
}
