using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;

/// <summary>
/// Enables conversion of empty strings to a null <see cref="DateTimeOffset"/>
/// </summary>
public class EmptyDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTimeOffset?));
        if (DateTimeOffset.TryParse(reader.GetString() ?? string.Empty, out DateTimeOffset result))
            return result;
        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
