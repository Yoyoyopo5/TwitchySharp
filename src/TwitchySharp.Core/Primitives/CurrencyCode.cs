using System;
using System.Collections.Frozen;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Represents a specific ISO-4217 currency code.
/// </summary>
/// <remarks>
/// Can be created from a <see cref="RegionInfo"/>.
/// </remarks>
[Wrapper<string>]
public readonly partial record struct CurrencyCode
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

    internal static CurrencyCode Create(string value) // Used by JsonConverter
        => TryParse(value, out CurrencyCode currencyCode) switch
        {
            true => currencyCode,
            _ => throw new JsonException($"Invalid ISO-4217 currency code {value} when deserializing currency code.")
        };
}
