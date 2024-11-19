using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Conduits.Models;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains a list of created conduits.
/// </summary>
public record CreateConduitsResponse
{
    /// <summary>
    /// The list of created conduits.
    /// </summary>
    public required Conduit[] Data { get; init; }
}
