using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;

/// <summary>
/// Simple RGB color container supporting to and from HTML hex color strings.
/// </summary>
/// <remarks>
/// Default JSON converter supports serializing and deserializing from hex strings.
/// </remarks>
[JsonConverter(typeof(HexColorJsonConverter))]
public readonly record struct RgbColor
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public RgbColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    /// Creates an <see cref="RgbColor"/> from a hex string (e.g. "#FF5733")
    /// </summary>
    /// <remarks>
    /// Empty or <see langword="null"/> inputs default to #000000.
    /// </remarks>
    public static RgbColor FromHex(string? hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            return new RgbColor(0, 0, 0);

        // Clean the string
        ReadOnlySpan<char> span = hex.AsSpan();
        if (span[0] == '#') span = span[1..];

        if (span.Length != 6 && span.Length != 8)
            throw new FormatException("Hex string must be 6 or 8 characters long.");

        // Parse components
        byte r = byte.Parse(span.Slice(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(span.Slice(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(span.Slice(4, 2), NumberStyles.HexNumber);

        return new RgbColor(r, g, b);
    }

    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}

internal class HexColorJsonConverter : JsonConverter<RgbColor>
{
    public override RgbColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.String => RgbColor.FromHex(reader.GetString()!),
            _ => throw new JsonException($"Unexpected {reader.TokenType} when deserializing hex color.")
        };

    public override void Write(Utf8JsonWriter writer, RgbColor value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());
}
