using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;
public class SnakeCaseLowerJsonStringEnumConverter<T>() 
    : JsonStringEnumConverter<T>(JsonNamingPolicy.SnakeCaseLower) 
    where T : struct, Enum; 
// Need to include because this version doesn't allow passing params to JsonConverter

public class SnakeCaseUpperJsonStringEnumConverter<T>()
    : JsonStringEnumConverter<T>(JsonNamingPolicy.SnakeCaseUpper)
    where T : struct, Enum;
