using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Models;
public record struct DateTimeOffsetRange
{
    [JsonInclude, JsonRequired]
    public DateTimeOffset StartedAt { get; private set; }
    [JsonInclude, JsonRequired]
    public DateTimeOffset EndedAt { get; private set; }
}
