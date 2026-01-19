using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api;

public record struct DateTimeOffsetRange
{
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? StartedAt { get; private set; }
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? EndedAt { get; private set; }
}
