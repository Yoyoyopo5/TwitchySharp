using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Conduits.Models;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains a list of conduits.
/// </summary>
public record GetConduitsResponse
{
    /// <summary>
    /// The list of conduits.
    /// </summary>
    public required Conduit[] Data { get; init; }
}