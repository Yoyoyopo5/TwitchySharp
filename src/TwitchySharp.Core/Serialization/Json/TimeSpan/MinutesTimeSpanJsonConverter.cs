using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Serialization;

/// <summary>
/// Converts a number representing minutes into an equivalent <see cref="TimeSpan"/>.
/// </summary>
public class MinutesTimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    /// <inheritdoc/>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.Number => TimeSpan.FromMinutes(reader.GetDouble()),
            JsonTokenType.String => reader.GetString() switch
            {
                string value => TimeSpan.FromMinutes(double.Parse(value)),
                _ => default
            },
            _ => throw new JsonException($"Unexpected token {reader.TokenType} when parsing TimeSpan.")
        };

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.TotalMinutes);
}
