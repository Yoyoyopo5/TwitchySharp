using System.Globalization;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Represents a specific ISO-4217 currency code.
/// </summary>
/// <remarks>
/// Can be created from a <see cref="RegionInfo"/>.
/// </remarks>
[Wrapper<string>]
public readonly partial record struct CurrencyCode(string Value)
{
    /// <summary>
    /// Extract the ISO currency symbol from a <see cref="RegionInfo"/>.
    /// </summary>
    /// <param name="regionInfo">The region info to get currency information from.</param>
    public CurrencyCode(RegionInfo regionInfo) : this(regionInfo.ISOCurrencySymbol) { }
}
