using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TwitchySharp.Api;
internal static class JsonConfig
{
    /// <summary>
    /// The default options used when serializing and deserializing API requests and responses.
    /// </summary>
    public readonly static JsonSerializerOptions ApiOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower, 
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull // This should be okay for writing optional params in requests, if something gets screwed up we can probably use an attribute to fix it for that case.
    };
}
