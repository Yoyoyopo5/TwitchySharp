using System;
using System.Collections.Frozen;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;

/// <summary>
/// Represents a specific ISO-4217 currency code.
/// </summary>
/// <remarks>
/// Can be created from a <see cref="RegionInfo"/>.
/// </remarks>
[JsonConverter(typeof(CurrencyCodeJsonConverter))]
public readonly record struct CurrencyCode : IWrapValue<string>
{
    private static readonly FrozenDictionary<string, RegionInfo> _currencyCodes = CultureInfo
        .GetCultures(CultureTypes.SpecificCultures)
        .Select(c => new RegionInfo(c.Name))
        .GroupBy(r => r.ISOCurrencySymbol)
        .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase)
        .ToFrozenDictionary();

    public string Value { get; }

    private CurrencyCode(string currencyCode)
        => Value = currencyCode;

    /// <summary>
    /// Extract the ISO currency symbol from a <see cref="RegionInfo"/>.
    /// </summary>
    /// <param name="regionInfo">The region info to get currency information from.</param>
    public CurrencyCode(RegionInfo regionInfo) : this(regionInfo.ISOCurrencySymbol) { }

    /// <summary>
    /// Try to parse a <see cref="CurrencyCode"/> from a string ISO-4217 currency code.
    /// </summary>
    /// <param name="currencyCode">The ISO-4217 currency code to parse.</param>
    /// <param name="value">The parsed value.</param>
    /// <returns>A <see langword="bool"/> indicating if the parsing was successful.</returns>
    public static bool TryParse(string currencyCode, out CurrencyCode value)
    {
        if (_currencyCodes.TryGetValue(currencyCode, out RegionInfo? cachedRegion))
        {
            value = new CurrencyCode(cachedRegion.ISOCurrencySymbol);
            return true;
        }
        value = default;
        return false;
    }

    public static implicit operator string(CurrencyCode currencyCode)
        => currencyCode.Value;
    public override string ToString()
        => Value;
}

internal class CurrencyCodeJsonConverter : JsonConverter<CurrencyCode>
{
    public override CurrencyCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() switch
        {
            string value => CurrencyCode.TryParse(value, out CurrencyCode currencyCode) switch
            {
                true => currencyCode,
                _ => throw new JsonException($"Invalid ISO-4217 currency code {value} when deserializing currency code.")
            },
            _ => throw new JsonException($"Unexpected {reader.TokenType} when deserializing currency code.")
        };

    public override void Write(Utf8JsonWriter writer, CurrencyCode value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}
