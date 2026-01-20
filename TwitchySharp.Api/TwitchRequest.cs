using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;
/// <summary>
/// A basic Twitch API request.
/// </summary>
/// <remarks>
/// This can be used directly with an <see cref="ITwitchClient"/> or converted to an <see cref="HttpRequestMessage"/> for manually handling.
/// </remarks>
public record TwitchRequest : ITwitchRequest
{
    /// <summary>
    /// The HTTP method to send the request with.
    /// </summary>
    public HttpMethod Method { get; init; } = HttpMethod.Get;
    /// <summary>
    /// The full uri of the request, including query.
    /// </summary>
    public Uri RequestUri { get; init; } = new("https://api.twitch.tv/"); // Should be overwritten by derived classes, but we put a default value in here to prevent a warning.
    /// <summary>
    /// The client id to use with the request, if any.
    /// </summary>
    /// <remarks>
    /// Most Helix endpoints require this. 
    /// It is added to the Options of the <see cref="HttpRequestMessage"/>.
    /// If using the <see cref="TwitchClient"/>, it will automatically be added as a header.
    /// If you are sending the <see cref="HttpRequestMessage"/> manually, use the <see cref="TwitchAuthorizationHandler"/> delegating handler
    /// or set the <c>Client-Id</c> header manually.
    /// </remarks>
    public ClientId? ClientId { get; init; }
    /// <summary>
    /// The access token (app or user) to use with the request, if any.
    /// </summary>
    /// <remarks>
    /// Most Helix endpoints require this. 
    /// It is added to the Options of the <see cref="HttpRequestMessage"/>.
    /// If using the <see cref="TwitchClient"/>, it will automatically be added as the Bearer authorization header value.
    /// If you are sending the <see cref="HttpRequestMessage"/> manually, use the <see cref="TwitchAuthorizationHandler"/> delegating handler
    /// or set the <c>Authorization</c> header manually.
    /// </remarks>
    public AccessToken? AccessToken { get; init; }
    /// <summary>
    /// The content (data) of the request, as an <see cref="object"/>.
    /// </summary>
    /// <remarks>
    /// This will be serialized into <see cref="JsonContent"/> when creating the <see cref="HttpRequestMessage"/>.
    /// Note that if <see cref="Content"/> is not <see langword="null"/>, this property will be ignored,
    /// and <see cref="Content"/> will be used as the content of the <see cref="HttpRequestMessage"/>.
    /// </remarks>
    public object? ContentObject { get; init; }
    /// <summary>
    /// The content (data) of the request.
    /// </summary>
    /// <remarks>
    /// Note that if this is not <see langword="null"/>, <see cref="ContentObject"/> will not be serialized as <see cref="JsonContent"/>.
    /// In other words, this property overrides <see cref="ContentObject"/> if it is set.
    /// </remarks>
    public HttpContent? Content { get; init; }
    /// <summary>
    /// Create a new <see cref="HttpRequestMessage"/> for this request.
    /// </summary>
    /// <remarks>
    /// Note that this does not set the <c>Client-Id</c> and <c>Authorization</c> headers required by many endpoints.
    /// The <see cref="ClientId"/> and <see cref="AccessToken"/> are added as <see cref="TwitchAuthorizationRequestOptions"/>
    /// into the Options property of the returned <see cref="HttpRequestMessage"/>. 
    /// Use the <see cref="TwitchRequestOptionsKeys.Authorization"/> key to access them and set the headers manually or use the
    /// <see cref="TwitchAuthorizationHandler"/> delegating handler.
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
        httpRequest.Options.Set(TwitchRequestOptionsKeys.Authorization, new TwitchAuthorizationRequestOptions
        {
            ClientId = ClientId?.Value,
            AccessToken = AccessToken?.Value
        });
        return httpRequest;
    }
}

/// <inheritdoc cref="TwitchRequest"/>
/// <typeparam name="TResponseContent">
/// The type the the response content should be deserialized into.
/// Note that this does nothing inside of this class on its own. 
/// It is used to infer return response types in an <see cref="ITwitchClient"/>.
/// </typeparam>
public record TwitchRequest<TResponseContent> : TwitchRequest, ITwitchRequest<TResponseContent>;
