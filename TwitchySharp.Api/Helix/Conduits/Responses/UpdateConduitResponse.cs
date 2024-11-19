using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains a list of updated conduits.
/// </summary>
public record UpdateConduitResponse
{
    /// <summary>
    /// A list of updated conduits.
    /// </summary>
    public required Conduit[] Data { get; init; }
}
