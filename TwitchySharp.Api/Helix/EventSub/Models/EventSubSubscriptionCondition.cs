using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace TwitchySharp.Api.Helix.EventSub;

internal record EventSubSubscriptionCondition
    : IReadOnlyDictionary<string, object>
{
    private ImmutableDictionary<string, object> _condition;

    public EventSubSubscriptionCondition()
        => _condition = ImmutableDictionary<string, object>.Empty;
    private EventSubSubscriptionCondition(ImmutableDictionary<string, object> condition)
        => _condition = condition;

    /// <summary>
    /// Sets a condition.
    /// </summary>
    /// <param name="conditionName">The name of the condition.</param>
    /// <param name="conditionValue">The value to set the condition to. If null, the condition is skipped.</param>
    /// <returns></returns>
    public EventSubSubscriptionCondition Set(string conditionName, object? conditionValue)
        => conditionValue is null ? this : new EventSubSubscriptionCondition(_condition.SetItem(conditionName, conditionValue));

    #region IReadOnlyDictionary
    public object this[string key] => ((IReadOnlyDictionary<string, object>)_condition)[key];

    public IEnumerable<string> Keys => _condition.Keys;

    public IEnumerable<object> Values => _condition.Values;

    public int Count => _condition.Count;

    public bool ContainsKey(string key) => _condition.ContainsKey(key);

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => ((IEnumerable<KeyValuePair<string, object>>)_condition).GetEnumerator();

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value) => _condition.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_condition).GetEnumerator();
    #endregion
}
