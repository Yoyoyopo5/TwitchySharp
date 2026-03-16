using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers;

public static class ThreadSafety
{
    private class LocalLockProvider<TKey>
        where TKey : notnull
    {
        private readonly ConcurrentDictionary<TKey, RefCountingSemaphore> _cache = new();
        public async ValueTask<IAsyncDisposable> AcquireAsync(TKey key, CancellationToken ct)
        {
            while (true)
            {
                RefCountingSemaphore wrapper = _cache.AddOrUpdate(key,
                _ => new RefCountingSemaphore { Semaphore = new(1, 1) },
                (_, existing) =>
                {
                    Interlocked.Increment(ref existing.RefCount);
                    return existing;
                });

                await wrapper.Semaphore.WaitAsync(ct).ConfigureAwait(false);

                if (_cache.TryGetValue(key, out RefCountingSemaphore? current) && current == wrapper)
                    return new Releaser(key, wrapper, _cache);

                wrapper.Semaphore.Release();
                if (Interlocked.Decrement(ref wrapper.RefCount) < 1)
                    _cache.TryRemove(new KeyValuePair<TKey, RefCountingSemaphore>(key, wrapper));
            }
        }

        private class RefCountingSemaphore
        {
            public required SemaphoreSlim Semaphore { get; init; }
            public int RefCount = 1;
        }

        private class Releaser(TKey key, RefCountingSemaphore wrapper, ConcurrentDictionary<TKey, RefCountingSemaphore> cache) : IAsyncDisposable
        {
            public ValueTask DisposeAsync()
            {
                wrapper.Semaphore.Release();
                if (Interlocked.Decrement(ref wrapper.RefCount) < 1)
                    cache.TryRemove(new KeyValuePair<TKey, RefCountingSemaphore>(key, wrapper));
                return ValueTask.CompletedTask;
            }
        }
    }

    /// <summary>
    /// Ensures the next function can only be run once at a time per unique <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="keySelector">A selector function that derives a <typeparamref name="TKey"/> from the input <typeparamref name="T"/>.</param>
    /// <param name="lockFactory">A factory function that returns a <see cref="IAsyncDisposable"/> for a given <typeparamref name="TKey"/>.</param>
    /// <returns>A function returning a function that calls its input within a per-key thread-safe semaphore block.</returns>
    public static Func<Func<T, CancellationToken, ValueTask<TResult>>, Func<T, CancellationToken, ValueTask<TResult>>> Concurrently<T, TKey, TResult>(
        Func<T, TKey> keySelector,
        Func<TKey, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null
        )
        where TKey : notnull
    {
        Func<TKey, CancellationToken, ValueTask<IAsyncDisposable>> lockFac = lockFactory is not null ? lockFactory : new LocalLockProvider<TKey>().AcquireAsync;
        return next => async (value, ct) =>
        {
            await using IAsyncDisposable lease = await lockFac(keySelector(value), ct).ConfigureAwait(false);
            return await next(value, ct).ConfigureAwait(false);
        };
    }

    /// <summary>
    /// Ensures the next function is only called once per <typeparamref name="TKey"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The key is released once the inner function fully evaluates. 
    /// This allows the function to be evaluated again, but it ensures only one evaluation occurs at one time.
    /// </para>
    /// <para>
    /// <b>Contagious Failures:</b> Exceptions will affect all callers with the same unique key.
    /// Only the first <see cref="CancellationToken"/> is honored, and if it cancels, all consumers using the same
    /// key receive <see cref="TaskCanceledException"/> (even if they have their own unique cancellation tokens).
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The input type.</typeparam>
    /// <typeparam name="TKey">The key type. Invocations that resolve the same key will receive the same <see cref="ValueTask"/>.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="keySelector">The key selector function.</param>
    /// <returns>A function returning a function warpping its input in a lazy evaluation.</returns>
    public static Func<Func<T, CancellationToken, ValueTask<TResult>>, Func<T, CancellationToken, ValueTask<TResult>>> Lazily<T, TKey, TResult>(
        Func<T, TKey> keySelector
        )
        where TKey : notnull
    {
        ConcurrentDictionary<TKey, Lazy<Task<TResult>>> cache = new();
        return next => (value, ct)
            => new ValueTask<TResult>(cache.GetOrAdd(keySelector(value), k => new Lazy<Task<TResult>>(async () =>
            {
                try
                {
                    return await next(value, ct).ConfigureAwait(false);
                }
                finally
                {
                    // We remove here so that new invocations can trigger the next function again.
                    // But callers that invoked during processing all get the same ValueTask.
                    cache.TryRemove(k, out _);
                }
            })).Value);
    }
}
