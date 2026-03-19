using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api;
/// <summary>
/// If possible, please use static <see cref="Scope"/> definitions provided by this class.
/// You can use this constructor to create a <see cref="Scope"/> when a static definition is not provided.
/// </summary>
/// <param name="Value">The Twitch scope string (e.g. "bits:read")</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<Scope, string>))]
public partial record Scope(string Value) : ValueBackedEnum<string>(Value);

public static class ScopeExtensions
{
    internal static string FormatScopes(this IEnumerable<Scope> scopes)
        => string.Join(' ', scopes.Select(s => s.Value)); // Spec says use "+", but does not accept URL encoded "%2B", it accepts "%20", however.

    /// <summary>
    /// Determines if the provided set of scopes contains at least one of the required scopes.
    /// </summary>
    /// <param name="scopes">
    /// The scopes to check.
    /// </param>
    /// <param name="needsOneOf">
    /// <paramref name="scopes"/> requires at least one of these scopes.
    /// If the set is empty, this method will always return true.
    /// </param>
    /// <returns>A <see langword="bool"/> that indicates whether <paramref name="scopes"/> has at least one <see cref="Scope"/> from <paramref name="needsOneOf"/>.</returns>
    public static bool HasRequiredScope(this IReadOnlySet<Scope> scopes, IReadOnlySet<Scope> needsOneOf)
        => needsOneOf.Count == 0 || scopes.Overlaps(needsOneOf);
}
