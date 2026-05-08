using System;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api;

public readonly record struct DateTimeOffsetRange
{
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? StartedAt { get; private init; }
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? EndedAt { get; private init; }
}
