using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace TwitchySharp.Api;
/// <summary>
/// A utility class used to send <see cref="TwitchApiRequest{TResponse}"/> to Twitch.
/// </summary>
/// <param name="rateLimiter">The rate limiter that will be used when sending requests. If <see langword="null"/>, no rate limiter will be used. Note that exceeding <see href="https://dev.twitch.tv/docs/api/guide/#twitch-rate-limits">Twitch API rate limits</see> will result in additional API requests returning HTTP status code 429.</param>
/// <param name="httpClient">The client that should be used to send the request. If <see langword="null"/>, a default <see cref="HttpClient"/> is used.</param>
public class TwitchApi(HttpClient? httpClient = null, RateLimiter ? rateLimiter = null)
{
    internal TwitchHttpClient HttpClient { get; private set; } = new(rateLimiter, httpClient);
    /// <summary>
    /// Sends an API request to Twitch and returns the response.
    /// </summary>
    /// <typeparam name="TResponse">The type that the response will be deserialized into.</typeparam>
    /// <param name="request">The request to send to Twitch.</param>
    /// <param name="ct"></param>
    /// <returns>
    /// A <see cref="ValueTask"/> containing a result that can either be a <typeparamref name="TResponse"/> or one of
    /// <see cref="OperationCanceledException"/>,
    /// <see cref="ArgumentNullException"/>,
    /// <see cref="InvalidOperationException"/>,
    /// <see cref="HttpRequestException"/>,
    /// <see cref="ApiException"/>
    /// </returns>
    public ValueTask<OneOf<TResponse, Exception>> SendRequestAsync<TResponse>(TwitchApiRequest<TResponse> request, CancellationToken ct = default) // Lets change this to tradiational exception throwing for release
        => HttpClient.SendAsync(request, ct);
}
