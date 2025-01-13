using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers.JsonConverters;

// Need to include because this version doesn't allow passing params to JsonConverter
public class SnakeCaseUpperJsonStringEnumConverter<T>()
    : JsonStringEnumConverter<T>(JsonNamingPolicy.SnakeCaseUpper)
    where T : struct, Enum;
