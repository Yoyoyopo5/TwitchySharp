using System;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp;

public readonly record struct DateTimeOffsetRange
{
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? StartedAt { get; init; }
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? EndedAt { get; init; }
}
