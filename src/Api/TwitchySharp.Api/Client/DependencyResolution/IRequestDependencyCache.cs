using System.Collections.Concurrent;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Represents a cache for storing and retreiving Twitch request dependencies across requests.
/// </summary>
/// <typeparam name="TKey">The cache key.</typeparam>
/// <typeparam name="TValue">The cache value.</typeparam>
public interface IRequestDependencyCache<TKey, TValue>
{
    /// <summary>
    /// Get a cached <typeparamref name="TValue"/> associated with the <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key to retreive a value for.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>
    /// A <see cref="ValueTask"/> containing the <typeparamref name="TValue"/> associated with the <paramref name="key"/> or
    /// <see langword="default"/> if the <paramref name="key"/> is not found in the cache.
    /// </returns>
    ValueTask<TValue?> GetOrDefault(TKey key, CancellationToken ct);
    /// <summary>
    /// Sets a cached value associated with the <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key to set the value for.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> containing the modified cache.</returns>
    ValueTask<IRequestDependencyCache<TKey, TValue>> Set(TKey key, TValue value, CancellationToken ct);
}

/// <summary>
/// Extensions for <see cref="ResolveRequestDependency{T}"/> relating to caching values externally.
/// </summary>
public static class ResolveRequestDependencyCacheExtensions
{
    /// <summary>
    /// Configure a resolver to use an external cache that is preserved across requests.
    /// </summary>
    /// <typeparam name="TKey">The cache key.</typeparam>
    /// <typeparam name="TDetails">The cache values.</typeparam>
    /// <param name="next">The resolver to add a cache to.</param>
    /// <param name="cache">The cache to use.</param>
    /// <param name="isValid">A function that determines whether the cached value is used.</param>
    /// <returns>A new resolver that combines <paramref name="next"/> and <paramref name="cache"/>.</returns>
    public static ResolveRequestDependency<TDetails?> WithCache<TKey, TDetails>(
        this ResolveRequestDependency<TDetails?> next,
        IRequestDependencyCache<TKey, TDetails> cache,
        Func<TDetails, bool> isValid
        )
        => (scope, ct) => scope.ResolveOrDefault<TKey?>(ct)
            .BindAsync(async key => key is null
                ? (TDetails?)default
                : await cache.GetOrDefault(key, ct) is TDetails cachedDetails && isValid(cachedDetails)
                ? cachedDetails
                : await next(scope, ct).MapAsync(async newDetails =>
                {
                    if (newDetails is not null)
                        await cache.Set(key, newDetails, ct);
                    return newDetails;
                }));
}

/// <summary>
/// A mutable concurrent cache backed by <see cref="ConcurrentDictionary{TKey, TValue}"/>.
/// </summary>
internal class InMemoryConcurrentCache<TKey, TValue>(ConcurrentDictionary<TKey, TValue>? dictionary = null)
    : IRequestDependencyCache<TKey, TValue>
    where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, TValue> _cache = dictionary ?? new();

    public ValueTask<TValue?> GetOrDefault(TKey key, CancellationToken ct)
        => ValueTask.FromResult(_cache.GetValueOrDefault(key));
    public ValueTask<IRequestDependencyCache<TKey, TValue>> Set(TKey key, TValue value, CancellationToken ct)
    {
        _cache.AddOrUpdate(key, value, (_, _) => value);
        return ValueTask.FromResult<IRequestDependencyCache<TKey, TValue>>(this);
    }
}
