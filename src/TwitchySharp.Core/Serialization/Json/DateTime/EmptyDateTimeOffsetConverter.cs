using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Serialization;

/// <summary>
/// Enables conversion of empty strings to a null <see cref="DateTimeOffset"/>
/// </summary>
public class EmptyDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTimeOffset?));
        return DateTimeOffset.TryParse(reader.GetString() ?? string.Empty, out DateTimeOffset result)
            ? result
            : null;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.HasValue switch
        {
            false => string.Empty,
            true => value.Value.ToString(value.Value.Offset == TimeSpan.Zero ? "yyyy-MM-ddTHH:mm:ss.fffffffZ" : "yyyy-MM-ddTHH:mm:ss.fffffffzzz")
        });
    }
}
