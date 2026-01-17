using TwitchySharp.Api.Models.Helix.Conduits.Models;

namespace TwitchySharp.Api.Models.Helix.Conduits.Responses;
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
