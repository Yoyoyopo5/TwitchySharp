using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains information about a channel's current Shield Mode status.
/// </summary>
public record GetShieldModeStatusResponse
{
    /// <summary>
    /// A list containing a single entry of a channel's Shield Mode status.
    /// </summary>
    public required ShieldModeStatus[] Data { get; init; }
}
