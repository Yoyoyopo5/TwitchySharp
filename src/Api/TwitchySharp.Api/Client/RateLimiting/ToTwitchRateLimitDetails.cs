using System.Net.Http.Headers;

namespace TwitchySharp.Api;

internal static class HttpResponseMessageTwitchExtensions
{
    /// <summary>
    /// A helper function to get rate limit information from Twitch API responses.
    /// </summary>
    /// <param name="twitchResponseHeaders">The HTTP response headers from the Twitch API response message.</param>
    /// <returns>
    /// An object that contains current rate limit details.
    /// Missing headers will be replaced with null values in the returned object.
    /// </returns>
    public static TwitchRateLimitDetails ToTwitchRateLimitDetails(this HttpResponseHeaders twitchResponseHeaders)
        => new()
        {
            Limit = int.TryParse(twitchResponseHeaders.GetFirstValueOrDefault("Ratelimit-Limit"), out int limit) switch
            {
                true => limit,
                false => null
            },
            Remaining = int.TryParse(twitchResponseHeaders.GetFirstValueOrDefault("Ratelimit-Remaining"), out int remaining) switch
            {
                true => remaining,
                false => null
            },
            Reset = long.TryParse(twitchResponseHeaders.GetFirstValueOrDefault("Ratelimit-Reset"), out long resetSeconds) switch
            {
                true => DateTimeOffset.FromUnixTimeSeconds(resetSeconds),
                false => null
            }
        };

    private static string? GetFirstValueOrDefault(this HttpHeaders headers, string name)
        => headers.TryGetValues(name, out IEnumerable<string>? values) ? values.FirstOrDefault() : null;
}
