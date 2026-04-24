using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Shared;

namespace TwitchySharp.Api;
/// <summary>
/// A basic Twitch API request.
/// </summary>
/// <remarks>
/// This can be used directly with an <see cref="ITwitchClient"/> or converted to an <see cref="HttpRequestMessage"/> for manually handling.
/// </remarks>
public abstract record TwitchRequest
{
    /// <summary>
    /// The HTTP method to send the request with.
    /// </summary>
    public abstract HttpMethod Method { get; }
    /// <summary>
    /// The full uri of the request, including query.
    /// </summary>
    public abstract Uri RequestUri { get; }
    /// <summary>
    /// The content (data) of the request, as an <see cref="object"/>.
    /// </summary>
    /// <remarks>
    /// This will be serialized into <see cref="JsonContent"/> when creating the <see cref="HttpRequestMessage"/>.
    /// Note that if <see cref="Content"/> is not <see langword="null"/>, this property will be ignored,
    /// and <see cref="Content"/> will be used as the content of the <see cref="HttpRequestMessage"/>.
    /// </remarks>
    public virtual object? ContentObject { get; }
    /// <summary>
    /// The content (data) of the request.
    /// </summary>
    /// <remarks>
    /// Note that if this is not <see langword="null"/>, <see cref="ContentObject"/> will not be serialized as <see cref="JsonContent"/>.
    /// In other words, this property overrides <see cref="ContentObject"/> if it is set.
    /// </remarks>
    public virtual HttpContent? Content { get; }
    /// <summary>
    /// Create a new <see cref="HttpRequestMessage"/> for this request.
    /// </summary>
    /// <returns>A new <see cref="HttpRequestMessage"/> that can be used to execute the Twitch API request.</returns>
    public virtual HttpRequestMessage ToHttpRequestMessage()
    {
        JsonSerializerOptions serializerOptions = JsonConfig.ApiOptions;
        HttpRequestMessage httpRequest = new()
        {
            Method = Method,
            RequestUri = RequestUri,
            Content = Content ?? ContentObject switch
            {
                { } content => JsonContent.Create(content, MediaTypeHeaderValue.Parse("application/json"), serializerOptions),
                _ => null
            }
        };
        return httpRequest;
    }

    internal virtual ValueTask<TwitchResponse> CreateResponse(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => ValueTask.FromResult(new TwitchResponse()
        {
            Request = this,
            StatusCode = httpResponse.StatusCode,
            RateLimitDetails = httpResponse.Headers.ToTwitchRateLimitDetails()
        });
}

/// <inheritdoc cref="TwitchRequest"/>
/// <typeparam name="TResponseContent">
/// The type the the response content should be deserialized into.
/// </typeparam>
public abstract record TwitchRequest<TResponseContent> : TwitchRequest
{
    internal sealed override async ValueTask<TwitchResponse> CreateResponse(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => new TwitchResponse<TResponseContent>()
        {
            Request = this,
            StatusCode = httpResponse.StatusCode,
            RateLimitDetails = httpResponse.Headers.ToTwitchRateLimitDetails(),
            Content = await ConvertResponseContent(await httpResponse.Content.ReadAsStreamAsync(ct), ct)
        };

    // The define how the response content stream is converted into TResponseContent.
    // We make them virtual because we want to define the "default" scheme of JSON deserialization (which covers most response types).
    // The JsonSerializerOptions are kept private to prevent a vestigial dependency if ConvertResponseContent is overriden.
    private JsonSerializerOptions ResponseContentSerializerOptions { get; init; } = JsonConfig.ApiOptions; // Somewhat problematic because some endpoints will override ConvertResponseContent with a method that does not require this dependency. Not a deal-breaker, but may be confusing.
    /// <summary>
    /// Defines how the HTTP response content stream is converted into <typeparamref name="TResponseContent"/>.
    /// </summary>
    /// <remarks>
    /// The default virtual implementation is JSON deserialization using <see cref="JsonSerializer"/> and <see cref="JsonConfig.ApiOptions"/>.
    /// Override this method to define a custom response content converter.
    /// </remarks>
    /// <param name="contentStream">The HTTP content stream.</param>
    /// <returns>The converted <typeparamref name="TResponseContent"/>.</returns>
    protected virtual ValueTask<TResponseContent> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => JsonSerializer.DeserializeAsync<TResponseContent>(contentStream, ResponseContentSerializerOptions, ct)!; // Might cause problems if Twitch decides to return "null", although if documented, we will make TResponseContent nullable.
}
