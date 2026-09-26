namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains information about a newly created stream marker.
/// </summary>
public record CreateStreamMarkerResponseContent
{
    /// <summary>
    /// A list containing the single stream marker that was created.
    /// </summary>
    public required StreamMarker[] Data { get; init; }
}
