using System.Collections.Concurrent;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

internal static class IAsyncDisposableExtensions
{
    public static async ValueTask<T> AwaitUsing<T>(
        this ValueTask<IAsyncDisposable> disposable,
        Func<ValueTask<T>> func
        )
    {
        await using IAsyncDisposable dispose = await disposable;
        return await func();
    }
}

internal class RequestSerializingMiddleware<TKey>(
    Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<RequestSerializingMiddleware<TKey>.SerializationKey>> resolveKey,
    Func<TKey, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null)
    : TwitchClient.Middleware
    where TKey : notnull
{
    private readonly Func<TKey, CancellationToken, ValueTask<IAsyncDisposable>> _lockFactory
        = lockFactory ?? ThreadSafety.CreateInMemoryLockProvider<TKey>();

    public readonly record struct SerializationKey(TKey Value, bool Serialize = true);

    protected override async ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> send, ITwitchRequestDependencyScope scope, CancellationToken ct)
    {
        SerializationKey key = await resolveKey(scope, ct);

        return !key.Serialize
            ? await send(scope, ct)
            : await _lockFactory(key.Value, ct).AwaitUsing(() => send(scope, ct));
    }
}

/// <summary>
/// Contains <see cref="TwitchClient"/> extensions for rate limiting.
/// </summary>
public static class TwitchRateLimiting
{
    private class RateLimitingMiddleware(TwitchRateLimitQueueOptions options) : TwitchClient.Middleware
    {
        protected override ValueTask<TwitchResponse<T>> SendAsync<T>(
            Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<T>>> send,
            ITwitchRequestDependencyScope scope,
            CancellationToken ct
            )
            => scope.ResolveOrDefault<ClientId?>(ct)
                .MapAsync(async clientId =>
                {
                    if (!clientId.HasValue)
                        return await send(scope, ct);

                    if (await options.Cache.GetOrDefault(clientId.Value, ct) is TwitchRateLimitDetails cachedDetails)
                        await cachedDetails.WaitFor(options.GetNow(), ct);

                    TwitchResponse<T> response = await send(scope, ct);

                    await scope.ResolveOrDefault<HttpResponseMessage>(ct).MapAsync(
                        async responseMessage =>
                        {
                            if (responseMessage is not null)
                                await options.Cache.Set(
                                    clientId.Value,
                                    responseMessage.Headers.ToTwitchRateLimitDetails(),
                                    ct);
                            return responseMessage;
                        });

                    return response;
                }).MatchAsync(
                    e => new TwitchResponse<T>(e),
                    r => r
                );
    }

    /// <summary>
    /// Send each <see cref="TwitchRequest"/> in series.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This disables parallel request sending through the configured <see cref="HttpClient"/>.
    /// Can be useful for strict rate limiting situations (in that case, call this after configuring rate limiting).
    /// </para>
    /// <para>
    /// This will only serialize up to resolving <see cref="HttpResponseMessage"/>.
    /// The final response conversion from <see cref="HttpResponseMessage"/> to <see cref="TwitchResponse{TResponseContent}"/>
    /// is not serialized.
    /// </para>
    /// </remarks>
    /// <param name="client">The client to serialize requests for.</param>
    /// <param name="lockFactory">
    /// The lock provider to use.
    /// Each request waits for an <see cref="IAsyncDisposable"/> before resolving <see cref="HttpResponseMessage"/>,
    /// disposing it once the response is resolved.
    /// If <see langword="null"/>, uses a default in-memory lock provider scoped to the client.
    /// </param>
    /// <returns>A new <see cref="TwitchClient"/> configured to serialize requests by their resolved <see cref="ClientId"/>.</returns>
    public static TwitchClient SerializeRequestsByClientId(
        this TwitchClient client,
        Func<ClientId, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null)
        => client with { ConfiguredMiddleware = client.ConfiguredMiddleware.Add(new RequestSerializingMiddleware<ClientId>((scope, ct)
                => scope.ResolveOrDefault<ClientId?>(ct).MatchAsync(
                    e => new RequestSerializingMiddleware<ClientId>.SerializationKey(default, false),
                    v => v.HasValue
                        ? new RequestSerializingMiddleware<ClientId>.SerializationKey(v.Value)
                        : new RequestSerializingMiddleware<ClientId>.SerializationKey(default, false)
                    ), lockFactory)) };

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
    /// Call <see cref="SerializeRequestsByClientId"/> after this to serialize requests based on <see cref="ClientId"/>, if necessary.
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
        return client with { ConfiguredMiddleware = client.ConfiguredMiddleware.Add(new RateLimitingMiddleware(options)) };
    }
}
