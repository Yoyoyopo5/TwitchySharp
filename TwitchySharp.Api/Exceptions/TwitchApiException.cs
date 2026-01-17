using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Core;

namespace TwitchySharp.Api.Exceptions;
public class TwitchApiException(string? message = null, Exception? innerException = null) 
    : Exception(message ?? $"Twitch API returned a non-success response code.", innerException)
{
    public required ITwitchRequest Request { get; init; }
    public required IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; init; }
    public required IReadOnlyDictionary<string, IEnumerable<string>> ContentHeaders { get; init; }
    public required HttpStatusCode StatusCode { get; init; }
    public required byte[] Content { get; init; }

    internal static async ValueTask<TwitchApiException> FromRequestResponseAsync(ITwitchRequest request, HttpResponseMessage response, CancellationToken ct = default)
        => new()
        {
            Request = request,
            StatusCode = response.StatusCode,
            Headers = response.Headers.ToDictionary(),
            ContentHeaders = response.Content.Headers.ToDictionary(),
            Content = await response.Content.ReadAsByteArrayAsync(ct)
        };
}
