using System;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api;

public static class HttpResponseMessageTwitchExtensions
{
    /// <summary>
    /// A helper function to get rate limit information from Twitch API responses.
    /// </summary>
    /// <param name="twitchApiResponse">The HTTP response message from the Twitch API.</param>
    /// <returns>An object that contains current rate limit details.</returns>
    /// <exception cref="InvalidOperationException">One or more rate limit headers could not be found in the response, or duplicate rate limit headers were found.</exception>
    /// <exception cref="FormatException">One or more rate limit headers could not be parsed into integers.</exception>
    /// <exception cref="OverflowException">One or more rate limit headers were larger than <see cref="int.MaxValue"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The rate limit reset header could not be parsed into a <see cref="DateTimeOffset"/>.</exception>
    public static TwitchRateLimitDetails GetTwitchRateLimitDetails(this HttpResponseMessage twitchApiResponse)
        => new()
        {
            Limit = int.Parse(twitchApiResponse.Headers.GetValues("Ratelimit-Limit").Single()),
            Remaining = int.Parse(twitchApiResponse.Headers.GetValues("Ratelimit-Remaining").Single()),
            Reset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(twitchApiResponse.Headers.GetValues("Ratelimit-Reset").Single()))
        };
}
