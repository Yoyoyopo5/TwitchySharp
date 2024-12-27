using System;

namespace TwitchySharp.Api.Helix.GuestStar;

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
