using System.Net.Http.Json;
using System.Text.Json;
using TwitchySharp.Serialization;

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
}

/// <inheritdoc cref="TwitchRequest"/>
/// <typeparam name="TResponseContent">
/// The type the the response content should be deserialized into.
/// </typeparam>
public abstract record TwitchRequest<TResponseContent> : TwitchRequest
{
    /// <summary>
    /// Defines how the HTTP response content stream is converted into <typeparamref name="TResponseContent"/>.
    /// </summary>
    /// <remarks>
    /// The default virtual implementation is JSON deserialization using <see cref="JsonSerializer"/> and <see cref="JsonConfig.ApiOptions"/>.
    /// Override this method to define a custom response content converter.
    /// </remarks>
    /// <param name="contentStream">The HTTP content stream.</param>
    /// <returns>The converted <typeparamref name="TResponseContent"/>.</returns>
    public virtual Func<Stream, CancellationToken, ValueTask<TResponseContent>>? ConvertResponseContent { get; init; }
}
