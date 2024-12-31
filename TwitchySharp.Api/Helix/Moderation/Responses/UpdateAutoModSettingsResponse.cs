using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains the list of updated AutoMod settings.
/// </summary>
public record UpdateAutoModSettingsResponse
{
    /// <summary>
    /// A list with a single entry of the channel's updated AutoMod settings.
    /// </summary>
    public required AutoModSettings[] Data { get; init; }
}
