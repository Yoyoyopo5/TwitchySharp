using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers;

public static class ThreadSafety
{
    // Use internally to allow eviction of semaphores with no waiting threads.
    private class RefCountingSemaphore
    {
        public required SemaphoreSlim Semaphore { get; init; }
        public int RefCount = 1;
    }

    /// <summary>
    /// Ensures the next function can only be run once at a time per unique <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    /// <param name="keySelector">A selector function that derives a <typeparamref name="TKey"/> from the input <typeparamref name="T"/>.</param>
    /// <param name="semaphoreFactory">A factory function that returns a <see cref="SemaphoreSlim"/> for a given <typeparamref name="T"/>. Use this to configure sempahore lease count.</param>
    /// <returns>A function returning a function that calls its input within a per-key thread-safe semaphore block.</returns>
    public static Func<Func<T, CancellationToken, ValueTask<TResult>>, Func<T, CancellationToken, ValueTask<TResult>>> Concurrently<T, TKey, TResult>(
        Func<T, TKey> keySelector,
        Func<T, SemaphoreSlim>? semaphoreFactory = null
        )
        where TKey : notnull
    {
        static SemaphoreSlim defaultSemaphoreFactory() => new(1);
        ConcurrentDictionary<TKey, RefCountingSemaphore> cache = new();
        return next => async (value, ct) =>
        {
            TKey key = keySelector(value);
            // Track refs to prevent mem leak.
            RefCountingSemaphore wrapper = cache.AddOrUpdate(
                key,
                _ => new RefCountingSemaphore { Semaphore = semaphoreFactory is null ? defaultSemaphoreFactory() : semaphoreFactory(value) },
                (_, existing) =>
                {
                    Interlocked.Increment(ref existing.RefCount);
                    return existing;
                });

            try
            {
                await wrapper.Semaphore.WaitAsync(ct).ConfigureAwait(false);
                try
                {
                    return await next(value, ct);
                }
                finally
                {
                    wrapper.Semaphore.Release();
                }
            }
            finally
            {
                if (Interlocked.Decrement(ref wrapper.RefCount) < 1)
                    cache.TryRemove(KeyValuePair.Create(key, wrapper));
            }
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
