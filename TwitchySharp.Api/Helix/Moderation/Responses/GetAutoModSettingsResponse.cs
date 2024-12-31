using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of AutoMod settings.
/// </summary>
public record GetAutoModSettingsResponse
{
    /// <summary>
    /// Contains a single entry of a channel's current AutoMod settings.
    /// </summary>
    public required AutoModSettings[] Data { get; init; }
}
