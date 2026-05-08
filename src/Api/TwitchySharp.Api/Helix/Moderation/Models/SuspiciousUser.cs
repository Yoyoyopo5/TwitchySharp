using System;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about a specific suspicious chatter.
/// </summary>
public record SuspiciousUser
{
    /// <summary>
    /// The id of the user.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the user has the suspicious status.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator who applied the last suspicious status.
    /// </summary>
    public required UserId ModeratorId { get; init; }
    /// <summary>
    /// The date and time when the status was last updated.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
    /// <summary>
    /// The type of suspicious user status.
    /// </summary>
    public required SuspiciousUserStatus Status { get; init; }
    /// <summary>
    /// An array of suspicious user types that this user represents.
    /// </summary>
    public required SuspiciousUserType[] Types { get; init; }
}
