using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Serialization;

/// <summary>
/// Converts a number representing seconds into an equivalent <see cref="TimeSpan"/>.
/// </summary>
public class SecondsTimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    /// <inheritdoc/>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.Number => TimeSpan.FromSeconds(reader.GetDouble()),
            JsonTokenType.String => reader.GetString() switch
            {
                string value => TimeSpan.FromSeconds(double.Parse(value)),
                _ => default
            },
            _ => throw new JsonException($"Unexpected token {reader.TokenType} when parsing TimeSpan.")
        };

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.TotalSeconds);
}
