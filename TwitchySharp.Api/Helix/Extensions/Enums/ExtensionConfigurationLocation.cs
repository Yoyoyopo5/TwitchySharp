using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Possible locations for an extension's configuration.
/// </summary>
public enum ExtensionConfigurationLocation
{
    /// <summary>
    /// The Extensions Configuration Service hosts the configuration.
    /// </summary>
    Hosted,
    /// <summary>
    /// The Extension Backend Service (EBS) hosts the configuration.
    /// </summary>
    Custom,
    /// <summary>
    /// The extension doesn't require configuration.
    /// </summary>
    None
}
