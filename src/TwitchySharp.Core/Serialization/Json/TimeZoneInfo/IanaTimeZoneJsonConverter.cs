using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Serialization;

/// <summary>
/// Converts between IANA Timezone Ids (e.g. <c>America/New_York</c>) and <see cref="TimeZoneInfo"/>.
/// </summary>
public class IanaTimeZoneJsonConverter : JsonConverter<TimeZoneInfo>
{
    public override TimeZoneInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.String => TimeZoneInfo.FindSystemTimeZoneById(reader.GetString()!),
            _ => throw new JsonException($"Unexpected {reader.TokenType} when deserializing Timezone.")
        };

    public override void Write(Utf8JsonWriter writer, TimeZoneInfo value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToIanaId());
}
