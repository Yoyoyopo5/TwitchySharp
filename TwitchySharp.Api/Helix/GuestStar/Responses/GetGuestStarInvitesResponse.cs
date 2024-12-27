using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains a list of invites for a specific Guest Star session.
/// </summary>
public record GetGuestStarInvitesResponse
{
    /// <summary>
    /// The list of invites.
    /// </summary>
    public required GuestStarInvite[] Data { get; init; }
}

/// <summary>
/// Contains data about a specific Guest Star invite.
/// </summary>
public record GuestStarInvite
{
    /// <summary>
    /// The user id of the invited guest.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The time when this invite was created.
    /// </summary>
    public required DateTimeOffset InvitedAt { get; init; }
    /// <summary>
    /// Status representing the invited user’s join state.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<GuestStarInviteStatus>))]
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

/// <summary>
/// Possible statuses for a Guest Star session invite.
/// </summary>
public enum GuestStarInviteStatus
{
    /// <summary>
    /// The user has been invited to the session but has not acknowledged it.
    /// </summary>
    Invited,
    /// <summary>
    /// The invited user has acknowledged the invite and joined the waiting room, but may still be setting up their media devices or otherwise preparing to join the call.
    /// </summary>
    Accepted,
    /// <summary>
    /// The invited user has signaled they are ready to join the call from the waiting room.
    /// </summary>
    Ready
}
