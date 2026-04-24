using System;

namespace TwitchySharp.Api.Helix.Raids;

/// <summary>
/// Contains information about the started raid.
/// </summary>
public record StartedRaid
{
    /// <summary>
    /// The date and time of when the raid was requested.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// Indicates whether the channel being raided contains mature content.
    /// </summary>
    public required bool IsMature { get; init; }
}
