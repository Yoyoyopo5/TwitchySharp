using System.Collections.Concurrent;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Contains pipeline extensions for adding rate limiting to a Twitch API request pipeline.
/// </summary>
public static class TwitchRateLimiting
{
    private readonly static ClientId EmptyClientId = new("");
    private static SendTwitchRequest SerializeRequestsByClientId(this SendTwitchRequest send, Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null)
    {
        var sendConcurrently = ThreadSafety.Serialize<TwitchRequestContext, ClientId, TwitchResponse>(ctx => ctx switch
        {
            TwitchAuthorizationRequestContext authContext => authContext.AuthorizationHeaders.ClientId ?? EmptyClientId,
            TwitchRequestContext context => (context.Request is IAuthorizedTwitchRequest authorizedRequest ? authorizedRequest.AuthorizationContext.Identity.ClientId : null) ?? EmptyClientId,
            _ => EmptyClientId
        }, lockFactory)(async (context, ct) => await send(context, ct));

        return (context, ct) => sendConcurrently(context, ct).AsTask();
    }

    /// <summary>
    /// Add rate limiting to a <see cref="SendTwitchRequest"/> pipeline.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Rate Limiting is not concurrent by default and does not guarantee an HTTP 429 Too Many Requests error will never be returned.
    /// Use <see cref="WithStrictRateLimiting(SendTwitchRequest, TwitchRateLimitQueueOptions, Func{ClientId, CancellationToken, ValueTask{IAsyncDisposable}}?)"/> to serialize requests based on <see cref="ClientId"/>.
    /// </para>
    /// <para>
    /// You may define your own rate limit cache via the <paramref name="options"/>.
    /// If you do not define a cache, a default in-memory <see cref="ConcurrentDictionary{TKey, TValue}"/> is used (fine for most use cases).
    /// </para>
    /// </remarks>
    /// <param name="send">The send pipeline to apply rate limiting to.</param>
    /// <param name="options">The rate limiter options.</param>
    /// <returns>A <see cref="SendTwitchRequest"/> function composed of <paramref name="send"/> and the rate limiter.</returns>
    public static SendTwitchRequest WithRateLimiting(
        this SendTwitchRequest send,
        TwitchRateLimitQueueOptions? options = null
        )
    {
        options ??= new();
        return async (context, ct) =>
        {
            ClientId clientId = (context is TwitchAuthorizationRequestContext authorizationContext
                ? authorizationContext.AuthorizationHeaders.ClientId
                : context.Request is IAuthorizedTwitchRequest authorizedRequest
                ? authorizedRequest.AuthorizationContext.Identity.ClientId
                : null)
                ?? EmptyClientId;

            if (await options.Cache.GetRateLimitDetails(clientId, ct) is TwitchRateLimitDetails cachedDetails
                && cachedDetails is { Remaining: 0, Reset: not null }
                && cachedDetails.Reset.Value > DateTimeOffset.UtcNow)
                await Task.Delay(cachedDetails.Reset.Value - DateTimeOffset.UtcNow + options.ClockSkew, ct);

            TwitchResponse response = await send(context, ct);
            if (response.RateLimitDetails is not null)
                await options.Cache.SetRateLimitDetails(
                    clientId,
                    response.RateLimitDetails.Value,
                    ct
                    );

            return response;
        };
    }

    /// <inheritdoc cref="WithRateLimiting(SendTwitchRequest, TwitchRateLimitQueueOptions?)"/>
    /// <param name="client">The client to add rate limiting to.</param>
    public static TwitchClient WithRateLimiting(
        this TwitchClient client,
        TwitchRateLimitQueueOptions? options = null
        )
        => client.With(send => send.WithRateLimiting(options));

    /// <summary>
    /// Add strict (concurrent) rate limiting to a <see cref="SendTwitchRequest"/> pipeline.
    /// </summary>
    /// <remarks>
    /// Requests will be serialized by <see cref="ClientId"/> to ensure the next request is not sent before rate limits are checked from the previous request.
    /// This incurs a throughput performance penalty but may be useful if you are receiving many HTTP 429 Too Many Requests errors.
    /// </remarks>
    /// <param name="send">The send pipeline to apply strict rate limiting to.</param>
    /// <param name="options">The rate limiter options.</param>
    /// <param name="lockFactory">The concurrency lock factory to use. If left <see langword="null"/>, a default in-memory provider is used.</param>
    public static SendTwitchRequest WithStrictRateLimiting(
        this SendTwitchRequest send,
        TwitchRateLimitQueueOptions options,
        Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null
        )
        => send
            .WithRateLimiting(options)
            .SerializeRequestsByClientId(lockFactory);

    /// <inheritdoc cref="WithStrictRateLimiting(SendTwitchRequest, TwitchRateLimitQueueOptions, Func{ClientId, CancellationToken, ValueTask{IAsyncDisposable}}?)"/>
    /// <param name="client">The client to add strict rate limiting to.</param>
    public static TwitchClient WithStrictRateLimiting(
        this TwitchClient client,
        TwitchRateLimitQueueOptions options,
        Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null
        )
        => client.With(send => send.WithStrictRateLimiting(options, lockFactory));
}
