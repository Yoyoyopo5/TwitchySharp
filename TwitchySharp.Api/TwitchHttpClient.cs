using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;
using TwitchySharp.Api.ApiResponseConverters;

namespace TwitchySharp.Api;
/// <summary>
/// Handles rate limits and dispatching API requests to Twitch.
/// </summary>
/// <param name="rateLimiter">The rate limiter that will be used when sending requests. If <see langword="null"/>, no rate limiter will be used. Note that exceeding <see href="https://dev.twitch.tv/docs/api/guide/#twitch-rate-limits">Twitch API rate limits</see> will result in additional API requests returning HTTP status code 429.</param>
/// <param name="httpClient">The client that should be used to send the request. If <see langword="null"/>, a default <see cref="HttpClient"/> is used.</param>
public class TwitchHttpClient(HttpClient? httpClient = null, RateLimiter ? rateLimiter = null)
{
    private readonly HttpClient _httpClient = httpClient ?? new();
    private readonly RateLimiter? _rateLimiter = rateLimiter;

    /// <summary>
    /// Sends an API request to Twitch.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/> containing a <typeparamref name="TResponse"/> representing the API response.
    /// </returns>
    /// <exception cref="ApiException"/>
    /// <inheritdoc cref="HttpClient.SendAsync(HttpRequestMessage, CancellationToken)" path="/exception"/>
    /// <inheritdoc cref="TaskExtensions.RateLimit{T}(Task{T}, RateLimiter?, int, CancellationToken)" path="/exception"/>
    internal async ValueTask<TResponse> SendAsync<TResponse>(TwitchApiRequest<TResponse> request, IConvertApiResponse converter, CancellationToken ct = default)
        => (await _httpClient.SendAsync(request.ToHttpRequest(), HttpCompletionOption.ResponseHeadersRead, ct).RateLimit(_rateLimiter, 1, ct).ConfigureAwait(false)) switch
        {
            HttpResponseMessage { IsSuccessStatusCode: true } response => await converter.Convert<TResponse>(response, ct),
            HttpResponseMessage { IsSuccessStatusCode: false } response => throw new ApiException($"Twitch API returned error code {response.StatusCode}: {await response.Content.ReadAsStringAsync()}", response)
        };
}

internal static class TaskExtensions
{
    /// <summary>
    /// Rate limits a task with the supplied rate limiter.
    /// </summary>
    /// <typeparam name="T">The result type of the task.</typeparam>
    /// <param name="task">The task to rate limit.</param>
    /// <param name="rateLimiter">The rate limiter to acquire from the limiter.</param>
    /// <param name="permitCount">The amount of permits to use.</param>
    /// <param name="ct">The cancellation token to use.</param>
    /// <returns>A new task that wraps the original <paramref name="task"/>.</returns>
    /// <exception cref="OperationCanceledException"/>
    public static async Task<T> RateLimit<T>(this Task<T> task, RateLimiter? rateLimiter, int permitCount = 1, CancellationToken ct = default)
    {
        if (rateLimiter is null) return await task;
        using RateLimitLease lease = await rateLimiter.AcquireAsync(permitCount, ct);
        return lease.IsAcquired switch
        {
            true => await task,
            false => throw new OperationCanceledException("Request was denied by the rate limiter.")
        };
    }
}
