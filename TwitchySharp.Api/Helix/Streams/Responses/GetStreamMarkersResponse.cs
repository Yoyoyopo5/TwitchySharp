using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains a list of stream markers.
/// </summary>
public record GetStreamMarkersResponse
{
    /// <summary>
    /// The list of markers grouped by the user that created the marks.
    /// </summary>
    public required UserStreamMarkers[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains a list of stream markers made by a specific user on a specific video.
/// </summary>
public record UserStreamMarkers
{
    /// <summary>
    /// The id of the user that created the markers.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the user that created the markers.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the user that created the markers.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The marked video.
    /// </summary>
    public required MarkedVideo[] Videos { get; init; }
}

/// <summary>
/// Contains information about a video with markers.
/// </summary>
public record MarkedVideo
{
    /// <summary>
    /// The id of the video.
    /// </summary>
    public required string VideoId { get; init; }
    /// <summary>
    /// The markers for the video.
    /// </summary>
    public required VideoMarker[] Markers { get; init; }
}

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
