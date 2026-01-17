using TwitchySharp.Api.Models.Helix.Conduits.Models;

namespace TwitchySharp.Api.Models.Helix.Conduits.Responses;
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
