using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Concurrent;

namespace TwitchySharp.Api.AuthorizationResolution;

internal static class Concurrent
{
    public static Func<Func<T, CancellationToken, ValueTask<TResult>>, Func<T, CancellationToken, ValueTask<TResult>>> UseConcurrent<T, TResult>(
        Func<T, SemaphoreSlim> resolveSemaphore
        )
        => next => async (value, ct) =>
        {
            SemaphoreSlim semaphore = resolveSemaphore(value);
            await semaphore.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                return await next(value, ct);
            }
            finally
            {
                semaphore.Release();
            }
        };
}