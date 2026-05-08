using System;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// Contains information about a specific muted segment in a Twitch video.
/// </summary>
public record MutedSegment
{
    /// <summary>
    /// The duration of the segment.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Duration { get; init; }
    /// <summary>
    /// The offset from the beginning of the video to where the muted segment begins.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Offset { get; init; }
}
