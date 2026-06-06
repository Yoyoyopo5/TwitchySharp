using System.Collections.Concurrent;

namespace TwitchySharp.Api;

internal class DefaultRateLimitCache : ITwitchRateLimitCache
{
    private readonly ConcurrentDictionary<ClientId, TwitchRateLimitDetails> _cache = [];

    public ValueTask<TwitchRateLimitDetails?> GetRateLimitDetails(ClientId clientId, CancellationToken ct)
        => ValueTask.FromResult<TwitchRateLimitDetails?>(_cache.TryGetValue(clientId, out TwitchRateLimitDetails details) ? details : null);
    public ValueTask SetRateLimitDetails(ClientId clientId, TwitchRateLimitDetails details, CancellationToken ct)
    {
        _cache.AddOrUpdate(clientId, details, (id, previous) => details);
        return ValueTask.CompletedTask;
    }
}
