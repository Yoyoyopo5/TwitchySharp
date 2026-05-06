using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;

public static class TwitchRateLimiting // Should consider putting this in another project.
{
    private static Func<TwitchRequestHandler, TwitchRequestHandler> CreateRateLimitQueueHandler(
        Func<ClientId, CancellationToken, ValueTask<TwitchRateLimitDetails?>> getRateLimitDetails,
        Func<ClientId, TwitchRateLimitDetails, CancellationToken, ValueTask> setRateLimitDetails,
        TimeSpan clockSkew,
        Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null)
    {
        // Create the concurrency cache once when the handler is created.
        var concurrently = ThreadSafety.Concurrently<TwitchRequestContext, ClientId, TwitchResponse>(ctx => ctx.AuthorizationHeaders.ClientId!.Value, lockFactory);
        return next =>
        {
            // Create the concurrent execution function once when the client is built.
            var concurrentlyRateLimitAndSend = concurrently(async (context, ct) =>
            {
                ClientId clientId = context.AuthorizationHeaders.ClientId!.Value;

                if (await getRateLimitDetails(clientId, ct) is TwitchRateLimitDetails cachedDetails
                    && cachedDetails is { Remaining: 0, Reset: not null }
                    && cachedDetails.Reset.Value > DateTimeOffset.UtcNow)
                    await Task.Delay(cachedDetails.Reset.Value - DateTimeOffset.UtcNow + clockSkew, ct);

                TwitchResponse response = await next(context, ct);
                if (response.RateLimitDetails is not null)
                    await setRateLimitDetails(
                        clientId,
                        response.RateLimitDetails.Value,
                        ct
                        );

                return response;
            });

            // This runs every request.
            return (context, ct) => context.AuthorizationHeaders.ClientId is null
                ? next(context, ct)
                : concurrentlyRateLimitAndSend(context, ct);
        };
    }

    /// <summary>
    /// Add rate limiting to the <see cref="ITwitchClient"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Rate Limiting is concurrent and based on the request's <see cref="ClientId"/> and uses Twitch rate limit HTTP response headers to gate incoming requests.
    /// Note that adding rate limiting limits the amount of concurrent requests with the same <see cref="ClientId"/> that will be sent at once time to one.
    /// This ensures that Twitch will never return an HTTP 429 Too Many Requests status code (unless you use multiple <see cref="ITwitchClient"/>s or a distributed architecture).
    /// </para>
    /// <para>
    /// You may define your own rate limit cache via the <paramref name="options"/>.
    /// If you do not define a cache, a default in-memory <see cref="ConcurrentDictionary{TKey, TValue}"/> is used (fine for most use cases).
    /// </para>
    /// </remarks>
    /// <param name="builder">The builder to apply rate limiting to.</param>
    /// <param name="options">The rate limiter options.</param>
    /// <returns>The <paramref name="builder"/> with rate limiting configured.</returns>
    public static ITwitchClientBuilder WithRateLimiting(this ITwitchClientBuilder builder, TwitchRateLimitQueueOptions? options = null)
    {
        var queueOptions = (options ?? new()) switch
        {
            { CacheOptions: not null } o => o,
            { CacheOptions: null } o => new ConcurrentDictionary<ClientId, TwitchRateLimitDetails>() switch
            {
                { } cache => o with
                {
                    CacheOptions = new TwitchRateLimitQueueCacheOptions
                    {
                        GetRateLimitDetails = (clientId, ct) => ValueTask.FromResult<TwitchRateLimitDetails?>(cache.TryGetValue(clientId, out TwitchRateLimitDetails cachedDetails) switch
                        {
                            true => cachedDetails,
                            _ => null
                        }),
                        SetRateLimitDetails = (clientId, newDetails, ct) =>
                        {
                            cache.AddOrUpdate(clientId, _ => newDetails, (_, _) => newDetails);
                            return ValueTask.CompletedTask;
                        }
                    }
                }
            }
        };

        builder.Use(CreateRateLimitQueueHandler(
            queueOptions.CacheOptions!.GetRateLimitDetails,
            queueOptions.CacheOptions!.SetRateLimitDetails,
            queueOptions.ClockSkew,
            queueOptions.LockFactory
            ));
        return builder;
    }
}

/// <summary>
/// Options for Twitch rate limiting.
/// </summary>
public record TwitchRateLimitQueueOptions
{
    /// <summary>
    /// The amount of extra time that will be waited after Twitch's rate limit reset time elapses.
    /// </summary>
    public TimeSpan ClockSkew { get; init; } = TimeSpan.FromMilliseconds(100);
    /// <summary>
    /// The rate limit cache options.
    /// </summary>
    public TwitchRateLimitQueueCacheOptions? CacheOptions { get; init; }
    /// <summary>
    /// The lock selector for request concurrency.
    /// </summary>
    /// <remarks>
    /// This may be useful if you have a distributed design.
    /// If left <see langword="null"/>, a default in-memory lock provider is used.
    /// Leave this <see langword="null"/> unless you know what you're doing.
    /// </remarks>
    public Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? LockFactory { get; init; }
}

/// <summary>
/// Options for a Twitch rate limit details cache.
/// </summary>
public record TwitchRateLimitQueueCacheOptions
{
    /// <summary>
    /// Get the current rate limit for a specific <see cref="ClientId"/>, if any exists.
    /// </summary>
    public required Func<ClientId, CancellationToken, ValueTask<TwitchRateLimitDetails?>> GetRateLimitDetails { get; init; }
    /// <summary>
    /// Set the current rate limit for a specific <see cref="ClientId"/>.
    /// </summary>
    public required Func<ClientId, TwitchRateLimitDetails, CancellationToken, ValueTask> SetRateLimitDetails { get; init; }
}
