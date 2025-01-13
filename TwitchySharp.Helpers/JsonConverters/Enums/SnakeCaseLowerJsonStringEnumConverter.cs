using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers.JsonConverters;
public class SnakeCaseLowerJsonStringEnumConverter<T>()
    : JsonStringEnumConverter<T>(JsonNamingPolicy.SnakeCaseLower)
    where T : struct, Enum;
