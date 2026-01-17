using System.Net.Http;
using System.Text.Json;

namespace TwitchySharp.Api;

/// <summary>
/// A Twitch request message that is able to be converted to an <see cref="HttpRequestMessage"/> instance.
/// </summary>
public interface ITwitchRequest
{
    /// <summary>
    /// Create a new <see cref="HttpRequestMessage"/> from the Twitch request.
    /// </summary>
    /// <param name="serializerOptions">The serializer options to use when serializing the request's content, if it is an object.</param>
    /// <returns>A new <see cref="HttpRequestMessage"/>.</returns>
    HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions); // Remember that HttpRequestMessage is not reusable, so a method is more appropriate than a property here.
}

/// <summary>
/// A Twitch request message with a strongly-typed response content.
/// </summary>
/// <typeparam name="TResponseContent">The response content type.</typeparam>
public interface ITwitchRequest<TResponseContent> : ITwitchRequest;