using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters.DateTime;

namespace TwitchySharp.Api.Models;
public record struct DateTimeOffsetRange
{
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? StartedAt { get; private set; }
    [JsonInclude, JsonRequired, JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? EndedAt { get; private set; }
}
