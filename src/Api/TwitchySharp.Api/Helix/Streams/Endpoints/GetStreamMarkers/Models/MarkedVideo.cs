using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains information about a video with markers.
/// </summary>
public record MarkedVideo
{
    /// <summary>
    /// The id of the video.
    /// </summary>
    public required VideoId VideoId { get; init; }
    /// <summary>
    /// The markers for the video.
    /// </summary>
    public required VideoMarker[] Markers { get; init; }
}
