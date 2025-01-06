using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains information about a newly created stream marker.
/// </summary>
public record CreateStreamMarkerResponse
{
    /// <summary>
    /// A list containing the single stream marker that was created.
    /// </summary>
    public required StreamMarker[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific stream marker.
/// </summary>
public record StreamMarker
{
    /// <summary>
    /// The id of the marker.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The date and time when the marker was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The relative offset in seconds of the marker from the beginning of the stream.
    /// </summary>
    public required int PositionSeconds { get; init; }
    /// <summary>
    /// A description that the user gave the marker to help them remember why they marked the location.
    /// </summary>
    public required string Description { get; init; }
}
