using TwitchySharp.Api.Models.Helix.Conduits.Models;

namespace TwitchySharp.Api.Models.Helix.Conduits.Responses;
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