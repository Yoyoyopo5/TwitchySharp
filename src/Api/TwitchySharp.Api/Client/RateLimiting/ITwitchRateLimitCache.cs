namespace TwitchySharp.Api;

/// <summary>
/// A Twitch API rate limit cache.
/// </summary>
public interface ITwitchRateLimitCache
{
    /// <summary>
    /// Get the current rate limit for a specific <see cref="ClientId"/>, if any exists.
    /// </summary>
    ValueTask<TwitchRateLimitDetails?> GetRateLimitDetails(ClientId clientId, CancellationToken ct);
    /// <summary>
    /// Set the current rate limit for a specific <see cref="ClientId"/>.
    /// </summary>
    ValueTask SetRateLimitDetails(ClientId clientId, TwitchRateLimitDetails details, CancellationToken ct);
}
