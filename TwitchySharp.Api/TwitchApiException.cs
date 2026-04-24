using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;
/// <summary>
/// A custom exception for Twitch API requests that return non-success HTTP status codes.
/// </summary>
/// <remarks>
/// Contains a basic snapshot of the HTTP response, including the status code, headers, and response body (as a byte array).
/// </remarks>
/// <param name="message">The string message to include, if any.</param>
/// <param name="innerException">The inner exception to include, if any.</param>
public class TwitchApiException(string? message = null, Exception? innerException = null)
    : Exception(message ?? $"Twitch API returned a non-success response code.", innerException)
{
    /// <summary>
    /// The <see cref="TwitchRequest"/> that was sent to receive this response.
    /// </summary>
    public required TwitchRequest Request { get; init; }
    /// <summary>
    /// The captured response headers.
    /// </summary>
    public required IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; init; }
    /// <summary>
    /// The captured response content headers.
    /// </summary>
    public required IReadOnlyDictionary<string, IEnumerable<string>> ContentHeaders { get; init; }
    /// <summary>
    /// The captured response status code.
    /// </summary>
    public required HttpStatusCode StatusCode { get; init; }
    /// <summary>
    /// The captured response content as a <see langword="byte"/> array.
    /// </summary>
    public required byte[] Content { get; init; }

    internal static async ValueTask<TwitchApiException> FromRequestResponseAsync(TwitchRequest request, HttpResponseMessage response, CancellationToken ct = default)
        => new()
        {
            Request = request,
            StatusCode = response.StatusCode,
            Headers = response.Headers.ToDictionary(),
            ContentHeaders = response.Content.Headers.ToDictionary(),
            Content = await response.Content.ReadAsByteArrayAsync(ct)
        };
}
