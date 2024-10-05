using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;
public class SnakeCaseJsonStringEnumConverter() : JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower); // Need to include because this version doesn't allow passing params to JsonConverter
