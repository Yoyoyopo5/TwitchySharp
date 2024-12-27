using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Contains a list with Guest Star session information.
/// </summary>
public record GetGuestStarSessionResponse
{
    /// <summary>
    /// A list with a single entry of the Guest Star session details.
    /// </summary>
    public required GuestStarSession[] Data { get; init; }
}

/// <summary>
/// Contains data about a specific Guest Star session.
/// </summary>
public record GuestStarSession
{
    /// <summary>
    /// The id of the session.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// A list of guests currently interacting with the Guest Star session.
    /// </summary>
    public required GuestStarSessionGuest[] Guests { get; init; }
}

/// <summary>
/// Contains data about a specific Guest Star session guest.
/// </summary>
public record GuestStarSessionGuest
{
    /// <summary>
    /// An id representing this guest’s slot assignment.
    /// </summary>
    /// <remarks>
    /// This id matches the id referenced in browser source links used in broadcasting software.
    /// <list type="bullet">
    /// <item>Host is always <c>"0"</c>.</item>
    /// <item>Guests are assigned consecutive ids (e.g., <c>"1"</c>, <c>"2"</c>, <c>"3"</c>).</item>
    /// <item>Screen share is represented as <c>"SCREENSHARE"</c>.</item>
    /// </list>
    /// </remarks>
    public required string SlotId { get; init; }
    /// <summary>
    /// Determines whether or not the guest is visible in the browser source in the host’s streaming software.
    /// </summary>
    public required bool IsLive { get; init; }
    /// <summary>
    /// User id of the guest assigned to this slot.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// Display name of the guest assigned to this slot.
    /// </summary>
    public required string UserDisplayName { get; init; }
    /// <summary>
    /// Login (username) of the guest assigned to this slot.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// Value from 0 to 100 representing the host’s volume setting for this guest.
    /// </summary>
    public required int Volume { get; init; }
    /// <summary>
    /// The time when this guest was assigned a slot in the session.
    /// </summary>
    public required DateTimeOffset AssignedAt { get; init; }
    /// <summary>
    /// Information about the guest’s audio settings.
    /// </summary>
    public required GuestStarSessionGuestMediaSettings AudioSettings { get; init; }
    /// <summary>
    /// Information about the guest’s video settings.
    /// </summary>
    public required GuestStarSessionGuestMediaSettings VideoSettings { get; init; }
}

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
