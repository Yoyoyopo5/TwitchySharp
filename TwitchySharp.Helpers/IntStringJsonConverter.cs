using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;

/// <summary>
/// Converts an <see langword="int"/> to a <see langword="string"/> during serialization.
/// Converts a <see langword="string"/> to an <see langword="int"/> during deserialization.
/// </summary>
public class IntStringJsonConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.GetString() is not string value)
            return default;
        return int.Parse(value);
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());
}
