using System.Collections.Concurrent;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Contains <see cref="TwitchClient"/> extensions for rate limiting.
/// </summary>
public static class TwitchRateLimiting
{
    private static async ValueTask<T> AwaitUsing<T>(
        this ValueTask<IAsyncDisposable> disposable,
        Func<ValueTask<T>> func
        )
    {
        await using IAsyncDisposable dispose = await disposable;
        return await func();
    }

    /// <summary>
    /// Send each <see cref="TwitchRequest"/> in series.
    /// </summary>
    /// <remarks>
    /// This disables parallel request sending through the configured <see cref="HttpClient"/>.
    /// Can be useful for strict rate limiting situations (in that case, call this after configuring rate limiting).
    /// </remarks>
    /// <param name="client">The client to seriazlize requests for.</param>
    /// <param name="lockFactory">
    /// The lock provider to use.
    /// Each request waits for an <see cref="IAsyncDisposable"/> before resolving <see cref="HttpResponseMessage"/>,
    /// disposing it once the response is resolved.
    /// If <see langword="null"/>, uses a default in-memory lock provider scoped to the client.
    /// </param>
    /// <param name="defaultClientId">
    /// The fallback <see cref="ClientId"/> to use if the request does not resolve a <see cref="ClientId"/>.
    /// If <see langword="null"/>, does not serialize requests that do not resolve a <see cref="ClientId"/>.
    /// </param>
    /// <returns>A new <see cref="TwitchClient"/> configured to serialize requests by their resolved <see cref="ClientId"/>.</returns>
    public static TwitchClient SerializeRequestsByClientId(
        this TwitchClient client,
        Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null,
        ClientId? defaultClientId = null)
        => client.Configure<TwitchClient, HttpResponseMessage?>(next =>
        {
            lockFactory ??= ThreadSafety.CreateInMemoryLockProvider<ClientId>();
            return (scope, ct) => scope.ResolveOrDefault<ClientId?>(ct)
                .MapAsync(clientId => clientId ?? defaultClientId)
                .BindAsync(clientId => clientId.HasValue
                    ? lockFactory(clientId.Value, ct).AwaitUsing(() => next(scope, ct))
                    : next(scope, ct));
        });

    private static ValueTask WaitFor(
        this TwitchRateLimitDetails rateLimitDetails,
        DateTimeOffset now,
        CancellationToken ct
        )
        => rateLimitDetails is { Remaining: 0, Reset: not null }
            && rateLimitDetails.Reset.Value > now
            ? new ValueTask(Task.Delay(rateLimitDetails.Reset.Value - now, ct))
            : ValueTask.CompletedTask;


    /// <summary>
    /// Add rate limiting to a <see cref="TwitchClient"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Rate Limiting is not thread-safe by default and does not guarantee an HTTP 429 Too Many Requests error will never be returned.
    /// Call <see cref="SerializeRequestsByClientId(TwitchClient, Func{ClientId, CancellationToken, ValueTask{IAsyncDisposable}}?, ClientId?)"/> after this to serialize requests based on <see cref="ClientId"/>, if necessary.
    /// </para>
    /// <para>
    /// You may define your own rate limit cache via the <paramref name="configure"/>.
    /// If you do not define a cache, a default in-memory <see cref="ConcurrentDictionary{TKey, TValue}"/> scoped to the client is used (fine for most use cases).
    /// </para>
    /// </remarks>
    /// <param name="client">The <see cref="TwitchClient"/> to apply rate limiting to.</param>
    /// <param name="configure">Configure the rate limiter options.</param>
    /// <returns>A <see cref="TwitchClient"/> with rate limiting configured.</returns>
    public static TwitchClient WithRateLimiting(
        this TwitchClient client,
        Func<TwitchRateLimitQueueOptions, TwitchRateLimitQueueOptions>? configure = null
        )
        {
            TwitchRateLimitQueueOptions options = configure is null ? new() : configure(new());
            return client.Configure<TwitchClient, HttpResponseMessage?>(next => (scope, ct) =>
                scope.ResolveOrDefault<ClientId?>(ct)
                    .BindAsync(async clientId =>
                    {
                        if (!clientId.HasValue)
                            return await next(scope, ct);

                        if (await options.Cache.GetRateLimitDetails(clientId.Value, ct) is TwitchRateLimitDetails cachedDetails)
                            await cachedDetails.WaitFor(options.GetNow(), ct);

                        return await next(scope, ct).MapAsync(async response =>
                        {
                            if (response is not null)
                                await options.Cache.SetRateLimitDetails(
                                    clientId.Value,
                                    response.Headers.ToTwitchRateLimitDetails(),
                                    ct);
                            return response;
                        });
                    }));
        }
}
