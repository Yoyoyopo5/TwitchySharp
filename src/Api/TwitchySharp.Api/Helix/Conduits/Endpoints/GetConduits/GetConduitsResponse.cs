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
