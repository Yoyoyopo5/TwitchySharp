using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Raids;
/// <inheritdoc cref="StartedRaid"/>.
public record StartRaidResponse
{
    /// <summary>
    /// A list that contains a single object with information about the pending raid.
    /// </summary>
    public required StartedRaid[] Data { get; init; }
}

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
