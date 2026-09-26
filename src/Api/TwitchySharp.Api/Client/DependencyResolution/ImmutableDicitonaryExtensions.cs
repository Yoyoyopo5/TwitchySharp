using System.Collections.Immutable;

namespace TwitchySharp.Api;

internal static class ImmutableDicitonaryExtensions
{
    internal static ImmutableDictionary<TKey, TValue> SetIfKeyNotPresent<TKey, TValue>(
    this ImmutableDictionary<TKey, TValue> dict,
    TKey key,
    TValue value
    )
    where TKey : notnull
    => dict.ContainsKey(key)
        ? dict
        : dict.SetItem(key, value);
}
