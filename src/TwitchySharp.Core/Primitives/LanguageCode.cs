using System;
using System.Globalization;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Represents a specific language code used by Twitch.
/// </summary>
/// <remarks>
/// Value can be "other" for languages that Twitch doesn't support.
/// </remarks>
[Wrapper<string>]
public readonly partial record struct LanguageCode(string Value)
{
    /// <summary>
    /// For languages Twitch doesn't support.
    /// </summary>
    public static LanguageCode Other { get; } = new("other");

    /// <summary>
    /// Gets the culture info associated with the language code.
    /// </summary>
    /// <returns>A <see cref="CultureInfo"/> associated with the language code.</returns>
    /// <exception cref="CultureNotFoundException"/>
    /// <exception cref="ArgumentNullException"/>
    public CultureInfo ToCultureInfo()
        => CultureInfo.GetCultureInfo(Value);
}
