using System;
using System.Collections.Frozen;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// Represents a specific ISO 639-1 two-letter language code.
/// </summary>
/// <remarks>
/// Value can be "other" for languages that Twitch doesn't support.
/// </remarks>
[Wrapper<string>]
public readonly partial record struct LanguageCode
{
    private const string OTHER = "other"; // For languages Twitch "doesn't support".
    private static readonly FrozenDictionary<string, CultureInfo> _languages = CultureInfo
        .GetCultures(CultureTypes.NeutralCultures)
        .Where(c => c.TwoLetterISOLanguageName.Length == 2)
        .GroupBy(c => c.TwoLetterISOLanguageName)
        .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase)
        .ToFrozenDictionary();

    /// <summary>
    /// For languages Twitch doesn't support.
    /// </summary>
    public static LanguageCode Other { get; } = new(OTHER);

    public string Value { get; }
    private LanguageCode(string languageCode)
        => Value = languageCode;

    /// <summary>
    /// Extract the ISO 639-1 two-letter language code from a <see cref="CultureInfo"/> object.
    /// </summary>
    /// <param name="cultureInfo">The culture info to get the language code from.</param>
    public LanguageCode(CultureInfo cultureInfo)
        => Value = cultureInfo.TwoLetterISOLanguageName;

    /// <summary>
    /// Try parsing a string ISO 639-1 two-letter language code into a <see cref="LanguageCode"/>.
    /// </summary>
    /// <param name="code">The ISO 639-1 two-letter language code.</param>
    /// <param name="value">The parsed object.</param>
    /// <returns>A <see langword="bool"/> indicating if the parsing was successful.</returns>
    public static bool TryParse(string code, out LanguageCode value)
    {
        if (code != null && _languages.TryGetValue(code, out CultureInfo? culture))
        {
            value = new LanguageCode(culture.TwoLetterISOLanguageName);
            return true;
        }
        if (string.Equals(OTHER, code, StringComparison.OrdinalIgnoreCase))
        {
            value = new LanguageCode(OTHER);
            return true;
        }
        value = default;
        return false;
    }

    internal static LanguageCode Create(string value)
        => TryParse(value, out LanguageCode languageCode) switch
        {
            true => languageCode,
            _ => throw new JsonException($"Failed to parse {value} when deserializing ISO 639-1 two-letter language code.")
        };
}
