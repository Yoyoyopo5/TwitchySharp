using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub;

internal record EventSubSubscriptionCondition
    : IReadOnlyDictionary<ConditionKey, object>
{
    private readonly ImmutableDictionary<ConditionKey, object> _condition;

    public EventSubSubscriptionCondition()
        => _condition = ImmutableDictionary<ConditionKey, object>.Empty;
    private EventSubSubscriptionCondition(ImmutableDictionary<ConditionKey, object> condition)
        => _condition = condition;

    /// <summary>
    /// Sets a condition.
    /// </summary>
    /// <param name="conditionName">The name of the condition.</param>
    /// <param name="conditionValue">The value to set the condition to. If null, the condition is skipped.</param>
    /// <returns>This instance.</returns>
    public EventSubSubscriptionCondition Set(ConditionKey conditionName, object? conditionValue)
        => conditionValue is null ? this : new EventSubSubscriptionCondition(_condition.SetItem(conditionName, conditionValue));

    #region IReadOnlyDictionary
    public object this[ConditionKey key] => ((IReadOnlyDictionary<ConditionKey, object>)_condition)[key];

    public IEnumerable<ConditionKey> Keys => _condition.Keys;

    public IEnumerable<object> Values => _condition.Values;

    public int Count => _condition.Count;

    public bool ContainsKey(ConditionKey key) => _condition.ContainsKey(key);

    public IEnumerator<KeyValuePair<ConditionKey, object>> GetEnumerator() => ((IEnumerable<KeyValuePair<ConditionKey, object>>)_condition).GetEnumerator();

    public bool TryGetValue(ConditionKey key, [MaybeNullWhen(false)] out object value) => _condition.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_condition).GetEnumerator();
    #endregion
}

internal static class EventSubSubscriptionConditionExtensions
{
    internal static IReadOnlyDictionary<ConditionKey, string> GetValue<TOut>(this IReadOnlyDictionary<ConditionKey, string> dict, ConditionKey key, out TOut? value, Func<string, TOut> select)
    {
        if (dict.TryGetValue(key, out string? stringValue))
            value = select(stringValue);
        value = default;
        return dict;
    }

    internal static Validation<IReadOnlyDictionary<ConditionKey, string>> GetRequiredValue<TOut>(this IReadOnlyDictionary<ConditionKey, string> dict, ConditionKey key, out TOut? value, Func<string, TOut> select)
    {
        if (dict.TryGetValue(key, out string? stringValue))
        {
            value = select(stringValue);
            return new Validation<IReadOnlyDictionary<ConditionKey, string>>(dict);
        }
        value = default;
        return new ConditionMissingRequiredKeyError(key);
    }

    internal static Validation<IReadOnlyDictionary<ConditionKey, string>> GetRequiredValue<TOut>(this Validation<IReadOnlyDictionary<ConditionKey, string>> dict, ConditionKey key, out TOut? value, Func<string, TOut> select)
    {
        value = dict.Match(
            e => default,
            d => d.TryGetValue(key, out string? stringValue)
                    ? select(stringValue)
                    : default
            );
        return dict.Bind(d => d.ContainsKey(key)
            ? new Validation<IReadOnlyDictionary<ConditionKey, string>>(d)
            : new ConditionMissingRequiredKeyError(key)
            );
    }

    internal static Validation<IReadOnlyDictionary<ConditionKey, string>> GetValue<TOut>(this Validation<IReadOnlyDictionary<ConditionKey, string>> dict, ConditionKey key, out TOut? value, Func<string, TOut> select)
    {
        value = dict.Match(
            e => default,
            d => d.TryGetValue(key, out string? stringValue)
                    ? select(stringValue)
                    : default
            );
        return dict;
    }
}
