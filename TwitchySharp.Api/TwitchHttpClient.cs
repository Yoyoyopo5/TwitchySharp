using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.RateLimiting;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;

namespace TwitchySharp.Api;
/// <summary>
/// Internal helper class to consolidate code for making HTTP requests.
/// </summary>
/// <param name="rateLimiter">The rate limiter to use.</param>
/// <param name="httpClient">The client used to dispatch requests.</param>
internal class TwitchHttpClient(RateLimiter? rateLimiter = null, HttpClient? httpClient = null)
{
    private readonly HttpClient _httpClient = httpClient ?? new();
    private readonly RateLimiter? _rateLimiter = rateLimiter;

    /// <summary>
    /// Sends the <paramref name="request"/> through the rate limiter.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns>
    /// A <see cref="ValueTask"/> containing an <see cref="HttpResponseMessage"/> or one of
    /// <see cref="OperationCanceledException"/>, 
    /// <see cref="ArgumentNullException"/>, 
    /// <see cref="InvalidOperationException"/>, 
    /// <see cref="HttpRequestException"/>.
    /// </returns>
    private async ValueTask<OneOf<HttpResponseMessage, Exception>> SendAsync(HttpRequestMessage request, CancellationToken ct = default)
    {
        try
        {
            using RateLimitLease? lease = _rateLimiter is not null ? await _rateLimiter.AcquireAsync(1, ct) : null;
            if (lease is not null && !lease.IsAcquired) return new OperationCanceledException("Request was denied by the rate limiter."); // Any better exception type?

            using HttpResponseMessage response = await _httpClient.SendAsync(request, ct);
            return response;
        }
        catch (Exception ex) { return ex; }
    }

    /// <returns>A <see cref="ValueTask"/> containing a <typeparamref name="TResponse"/> representing the API response,
    /// or one of
    /// <see cref="OperationCanceledException"/>, 
    /// <see cref="ArgumentNullException"/>, 
    /// <see cref="InvalidOperationException"/>, 
    /// <see cref="HttpRequestException"/>, 
    /// <see cref="ApiException"/>
    /// </returns>
    public async ValueTask<OneOf<TResponse, Exception>> SendAsync<TResponse>(TwitchApiRequest<TResponse> request, CancellationToken ct = default)
    {
        OneOf<HttpResponseMessage, Exception> httpResponse = await SendAsync(request.ToHttpRequest(), ct);
        // If sending the request caused an exception, pass the exception upwards
        if (httpResponse.IsT1) return httpResponse.AsT1;

        // If the response is not a success status code, return an ApiException
        if (!httpResponse.AsT0.IsSuccessStatusCode) return new ApiException($"{httpResponse.AsT0.StatusCode} non-success response from Twitch API: {await httpResponse.AsT0.Content.ReadAsStringAsync()}", httpResponse.AsT0);

        OneOf<JsonElement, Exception> json = await httpResponse.AsT0.Content.ReadAsJsonAsync();

        // If the response fails to parse into JSON, return an ApiException.
        if (json.IsT1) return new ApiException($"", httpResponse.AsT0, json.AsT1);

        try
        {
            if (json.AsT0.Deserialize<TResponse>(JsonConfig.ApiOptions) is TResponse apiResponse)
                return apiResponse;
            // If the content of the response is a null literal (null JSON root value), wrap into exception.
            // I'm not aware of any situation where a null literal is returned by Twitch API.
            // Returning this as an exception instead of making return type nullable should reduce boilerplate in consumer code.
            return new ApiException($"Request was successful, but the response was a null literal value.", httpResponse.AsT0);
        }
        // If the response fails to deserialize (e.g. a required JSON value was not present) return an ApiException 
        catch (Exception ex) { return new ApiException($"", httpResponse.AsT0, ex); }
    }
}

internal static class HttpJsonExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContent"></param>
    /// <param name="ct"></param>
    /// <returns>
    /// A result containing either a <see cref="JsonElement"/> parsed from the <paramref name="httpContent"/> 
    /// or one of 
    /// <see cref="JsonException"/>, 
    /// <see cref="ArgumentException"/>
    /// </returns>
    public async static ValueTask<OneOf<JsonElement, Exception>> ReadAsJsonAsync(this HttpContent httpContent)
    {
        try
        {
            using JsonDocument json = JsonDocument.Parse(await httpContent.ReadAsStreamAsync());
            return json.RootElement.Clone();
        }
        catch (Exception ex) { return ex; }
    }
}
