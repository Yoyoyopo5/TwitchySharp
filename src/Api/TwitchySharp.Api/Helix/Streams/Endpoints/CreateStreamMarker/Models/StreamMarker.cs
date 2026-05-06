using System;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains information about a specific stream marker.
/// </summary>
public record StreamMarker
{
    /// <summary>
    /// The id of the marker.
    /// </summary>
    public required StreamMarkerId Id { get; init; }
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
