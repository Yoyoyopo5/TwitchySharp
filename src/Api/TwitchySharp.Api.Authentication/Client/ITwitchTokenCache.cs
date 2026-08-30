using System.Collections.Concurrent;

namespace TwitchySharp.Api.Authentication;

public interface ITwitchTokenCache<TKey, TValue>
{
    ValueTask<TValue?> GetOrDefault(TKey key, CancellationToken ct);
    ValueTask<ITwitchTokenCache<TKey, TValue>> Set(TKey key, TValue value);
}

internal class InMemoryConcurrentCache<TKey, TValue>(ConcurrentDictionary<TKey, TValue>? dictionary = null)
    : ITwitchTokenCache<TKey, TValue>
    where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, TValue> _cache = dictionary ?? new();

    public ValueTask<TValue?> GetOrDefault(TKey key, CancellationToken ct)
        => ValueTask.FromResult(_cache.GetValueOrDefault(key));
    public ValueTask<ITwitchTokenCache<TKey, TValue>> Set(TKey key, TValue value)
    {
        _cache.AddOrUpdate(key, value, (_, _) => value);
        return ValueTask.FromResult<ITwitchTokenCache<TKey, TValue>>(this);
    }
}
