using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace TwitchySharp.Api;
/// <summary>
/// A basic Twitch API request.
/// </summary>
/// <remarks>
/// This can be used directly with an <see cref="ITwitchClient"/> or converted to an <see cref="HttpRequestMessage"/> for manually handling.
/// </remarks>
public abstract record TwitchRequest : ITwitchRequest
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
    /// <remarks>
    /// THe <see cref="HttpRequestMessage.Options"/> will contain this instance of <see cref="TwitchRequest"/> under <see cref="TwitchRequestOptionsKeys.TwitchRequest"/>.
    /// </remarks>
    /// <param name="serializerOptions">The JSON serializer options to use if <see cref="ContentObject"/> is serialized as <see cref="JsonContent"/>.</param>
    /// <returns>A new <see cref="HttpRequestMessage"/> that can be used to execute the Twitch API request.</returns>
    public virtual HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions? serializerOptions = null)
    {
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
        httpRequest.Options.Set(TwitchRequestOptionsKeys.TwitchRequest, this);
        return httpRequest;
    }
}

/// <inheritdoc cref="TwitchRequest"/>
/// <typeparam name="TResponseContent">
/// The type the the response content should be deserialized into.
/// Note that this does nothing inside of this class on its own. 
/// It is used to infer return response types in an <see cref="ITwitchClient"/>.
/// </typeparam>
public abstract record TwitchRequest<TResponseContent> : TwitchRequest, ITwitchRequest<TResponseContent>;
