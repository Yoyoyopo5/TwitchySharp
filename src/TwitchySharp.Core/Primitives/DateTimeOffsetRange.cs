using System;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp;

/// <summary>
/// Represents a range between two specific <see cref="DateTimeOffset"/>s.
/// </summary>
public readonly record struct DateTimeOffsetRange
{
    /// <summary>
    /// The beginning of the range.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? StartedAt { get; init; }
    /// <summary>
    /// The end of the range.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? EndedAt { get; init; }
}
