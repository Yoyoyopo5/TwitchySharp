namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains a list of updated conduits.
/// </summary>
public record UpdateConduitResponseContent
{
    /// <summary>
    /// A list of updated conduits.
    /// </summary>
    public required Conduit[] Data { get; init; }
}
