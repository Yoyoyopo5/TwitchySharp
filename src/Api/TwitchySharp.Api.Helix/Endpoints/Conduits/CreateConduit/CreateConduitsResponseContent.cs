namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains a list of created conduits.
/// </summary>
public record CreateConduitsResponseContent
{
    /// <summary>
    /// The list of created conduits.
    /// </summary>
    public required Conduit[] Data { get; init; }
}
