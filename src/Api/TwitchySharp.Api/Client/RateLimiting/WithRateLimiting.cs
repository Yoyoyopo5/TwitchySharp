using System.Collections.Concurrent;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Contains <see cref="TwitchClient"/> extensions for rate limiting.
/// </summary>
public static class TwitchRateLimiting
{
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
        => client.Configure<HttpResponseMessage>(next =>
        {
            lockFactory ??= ThreadSafety.CreateInMemoryLockProvider<ClientId>();
            return async(context, ct) =>
            {
                (ClientId? clientId, RequestDependencyScope nextContext, Error? error)
                    = await context.GetOrDefault<ClientId?>(ct);

                if (error is not null)
                    return new DependencyResult<HttpResponseMessage>(error, nextContext);

                clientId ??= defaultClientId;

                // Skip serialize if no client id for request
                if (!clientId.HasValue)
                    return await next(nextContext, ct);

                await using IAsyncDisposable @lock = await lockFactory(clientId.Value, ct);
                return await next(nextContext, ct);
            };
        });

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
            return client.Configure<HttpResponseMessage>(next => async (context, ct) =>
            {
                (ClientId? clientId, RequestDependencyScope nextContext, Error? error)
                    = await context.GetOrDefault<ClientId?>(ct);

                if (error is not null)
                    return new DependencyResult<HttpResponseMessage>(error, nextContext);

                if (!clientId.HasValue)
                    return await next(context, ct);

                // We queue here
                if (await options.Cache.GetRateLimitDetails(clientId.Value, ct) is TwitchRateLimitDetails cachedDetails
                    && cachedDetails is { Remaining: 0, Reset: not null }
                    && cachedDetails.Reset.Value > DateTimeOffset.UtcNow)
                        await Task.Delay(cachedDetails.Reset.Value - DateTimeOffset.UtcNow + options.ClockSkew, ct);

                DependencyResult<HttpResponseMessage> responseResult = await next(context, ct);

                if (responseResult.Error is not null)
                    return responseResult;

                await options.Cache.SetRateLimitDetails(clientId.Value, responseResult.Value?.Headers.ToTwitchRateLimitDetails(), ct);

                return responseResult;
            });
        }
}
