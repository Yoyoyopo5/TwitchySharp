using System.Collections.Concurrent;

namespace TwitchySharp.Api;

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
    /// <remarks>
    /// If left <see langword="null"/>, a default in-memory <see cref="ConcurrentDictionary{TKey, TValue}"/> scoped to this options instance is used (fine for most use cases).
    /// </remarks>
    public ITwitchRateLimitCache Cache { get; init; } = new InMemoryRateLimitCache();
}
