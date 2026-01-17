using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using TwitchySharp.Api.Core;
using TwitchySharp.Api.Extensions;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Handlers;

/// <summary>
/// Resolves and updates <see cref="TwitchRateLimitDetails"/> based on client id.
/// </summary>
/// <remarks>
/// See <see cref="DefaultTwitchRateLimitResolver"/>.
/// </remarks>
public interface IResolveTwitchRateLimits
{
    ValueTask<TwitchRateLimitDetails> GetRateLimit(string clientId);
    ValueTask SetRateLimit(string clientId, TwitchRateLimitDetails rateLimit);
}

/// <summary>
/// Uses a <see cref="ConcurrentDictionary{TKey, TValue}"/> to store rate limit details for specific client ids.
/// </summary>
public class DefaultTwitchRateLimitResolver
    : IResolveTwitchRateLimits
{
    private readonly ConcurrentDictionary<string, TwitchRateLimitDetails> _rateLimits = [];
    public ValueTask<TwitchRateLimitDetails> GetRateLimit(string clientId)
        => ValueTask.FromResult(_rateLimits.GetValueOrDefault(clientId));
    public ValueTask SetRateLimit(string clientId, TwitchRateLimitDetails rateLimit)
    {
        _rateLimits[clientId] = rateLimit;
        return ValueTask.CompletedTask;
    }
}

/// <summary>
/// Automatically delays requests based on received rate limit information from the Twitch API.
/// </summary>
/// <remarks>
/// Rate limits should be resolved via client id.
/// Rate limits should be resolved via client id.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/guide#twitch-rate-limits">Twitch Rate Limits</see> for more information.
/// </remarks>
/// <param name="rateLimitResolver">The resolver to use for getting cached rate limit information.</param>
public class TwitchRateLimitingHandler(IResolveTwitchRateLimits rateLimitResolver) : DelegatingHandler
{
    private const string GLOBAL_KEY = "GLOBAL";
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct = default)
    {
        string rateLimiterKey = GetResourceKey(request);
        TwitchRateLimitDetails rateLimit = await rateLimitResolver.GetRateLimit(rateLimiterKey);
        int? remaining = rateLimit.Remaining;
        DateTimeOffset? limitResetsAt = rateLimit.Reset;

        if (!remaining.HasValue || !limitResetsAt.HasValue)
            return await SendAndUpdateRateLimiterAsync(rateLimiterKey, request, rateLimitResolver, ct);

        if (remaining.Value > 0)
            return await SendAndUpdateRateLimiterAsync(rateLimiterKey, request, rateLimitResolver, ct);

        if (DateTimeOffset.UtcNow < limitResetsAt.Value)
            await Task.Delay(limitResetsAt.Value - DateTimeOffset.UtcNow, ct);
        
        return await SendAndUpdateRateLimiterAsync(rateLimiterKey, request, rateLimitResolver, ct);
    }

    private async Task<HttpResponseMessage> SendAndUpdateRateLimiterAsync(string clientId, HttpRequestMessage request, IResolveTwitchRateLimits rateLimitResolver, CancellationToken ct = default)
    {
        HttpResponseMessage response = await base.SendAsync(request, ct);
        await rateLimitResolver.SetRateLimit(clientId, response.Headers.ToTwitchRateLimitDetails());
        return response;
    }

    private static string GetResourceKey(HttpRequestMessage request)
        => request.Options.TryGetValue(TwitchRequestOptionsKeys.Authorization, out TwitchAuthorizationRequestOptions? authOptions) switch
        {
            true => string.IsNullOrEmpty(authOptions.ClientId) ? GLOBAL_KEY : authOptions.ClientId,
            false => GLOBAL_KEY
        };
}
