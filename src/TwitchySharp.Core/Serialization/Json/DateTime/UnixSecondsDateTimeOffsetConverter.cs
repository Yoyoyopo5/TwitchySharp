using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Serialization;

/// <summary>
/// Converts <see cref="DateTimeOffset"/> to UNIX timestamp in seconds.
/// </summary>
public class UnixSecondsDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    /// <inheritdoc/>
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTimeOffset.FromUnixTimeSeconds(reader.TokenType switch
        {
            JsonTokenType.Number => reader.GetInt64(),
            JsonTokenType.String => long.Parse(reader.GetString() ?? "0"),
            _ => throw new NotSupportedException("Cannot parse input as a DateTimeOffset.")
        });

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.ToUnixTimeSeconds());
}
