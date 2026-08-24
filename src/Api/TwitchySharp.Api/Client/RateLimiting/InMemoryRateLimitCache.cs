using System.Collections.Concurrent;

namespace TwitchySharp.Api;

internal class InMemoryRateLimitCache : ITwitchRateLimitCache
{
    private readonly ConcurrentDictionary<ClientId, TwitchRateLimitDetails> _cache = [];

    public ValueTask<TwitchRateLimitDetails?> GetRateLimitDetails(ClientId clientId, CancellationToken ct)
        => ValueTask.FromResult<TwitchRateLimitDetails?>(_cache.TryGetValue(clientId, out TwitchRateLimitDetails details) ? details : null);
    public ValueTask SetRateLimitDetails(ClientId clientId, TwitchRateLimitDetails? details, CancellationToken ct)
    {
        if (details.HasValue)
            _cache.AddOrUpdate(clientId, details.Value, (id, previous) => details.Value);
        else // f-ing side effects
            _cache.TryRemove(clientId, out _);
        return ValueTask.CompletedTask;
    }
}
