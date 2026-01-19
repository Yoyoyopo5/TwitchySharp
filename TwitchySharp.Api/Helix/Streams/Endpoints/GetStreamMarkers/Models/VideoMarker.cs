using System;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains information about a specific video marker.
/// </summary>
public record VideoMarker
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
    /// The user-generated description for the marker.
    /// This is an empty string if a description was not provided when the marker was created.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// The relative offset in seconds of the marker from the beginning of the stream.
    /// </summary>
    public required int PositionSeconds { get; init; }
    /// <summary>
    /// A URL that can be used to open the video in Twitch Highlighter.
    /// </summary>
    [JsonPropertyName("URL")]
    public required string Url { get; init; }
}
