using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains information about a channel's Shield Mode status.
/// </summary>
public record UpdateShieldModeStatusResponse
{
    /// <summary>
    /// A list containing a single object of the channel's Shield Mode status.
    /// </summary>
    public required ShieldModeStatus[] Data { get; init; }
}

/// <summary>
/// Contains information about a channel's Shield Mode status.
/// </summary>
public record ShieldModeStatus
{
    /// <summary>
    /// Determines whether Shield Mode is active. 
    /// Is <see langword="true"/> if Shield Mode is active; otherwise, <see langword="false"/>.
    /// </summary>
    public required bool IsActive { get; init; }
    /// <summary>
    /// The user id of the moderator that last activated Shield Mode.
    /// </summary>
    public required string ModeratorId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that last activated Shield Mode.
    /// </summary>
    public required string ModeratorLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that last activated Shield Mode.
    /// </summary>
    public required string ModeratorName { get; init; }
    /// <summary>
    /// The date and time when Shield Mode was last activated.
    /// </summary>
    public required DateTimeOffset LastActivatedAt { get; init; }
}
