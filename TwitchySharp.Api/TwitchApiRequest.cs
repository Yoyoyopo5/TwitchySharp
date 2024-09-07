using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api;
public abstract class TwitchApiRequest<TResponse>(string path)
{
    /// <summary>
    /// The client id of the application. Used to form the header of the request.
    /// </summary>
    public string? ClientId { get; set; }
    /// <summary>
    /// The access token for the request. This can either be an app access token or a user authorization token depending on the request.
    /// </summary>
    public string? AccessToken { get; set; }
    /// <summary>
    /// The base url that the request should be routed to, usually <see href="https://api.twitch.tv"/>
    /// </summary>
    public abstract string BaseUrl { get; }
    /// <summary>
    /// The path to the api endpoint that follows the <see cref="BaseUrl"/>.
    /// </summary>
    public string Path { get; } = path;
    /// <summary>
    /// The full uri that the api request should be sent to.
    /// </summary>
    public string Uri => BaseUrl + Path;
    /// <summary>
    /// The data that should be attached to POST requests.
    /// </summary>
    public abstract string? Data { get; }
    /// <summary>
    /// The <see cref="HttpMethod"/> that the request will use.
    /// </summary>
    public abstract HttpMethod Method { get; }
    /// <summary>
    /// The <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Type">Content-Type</see> MIME type that will be set in the request header. 
    /// </summary>
    public abstract string? ContentType { get; }

    /// <summary>
    /// Creates a new <see cref="HttpRequestMessage"/> for the API request.
    /// </summary>
    /// <returns>An <see cref="HttpRequestMessage"/> that can be sent through an <see cref="HttpClient"/>.</returns>
    internal HttpRequestMessage ToHttpRequest()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = Method,
            RequestUri = new Uri(Uri),
            Content = new StringContent(Data)
        };

        // Headers
        if (ContentType is not null)
            httpRequest.Headers.Add("Content-Type", ContentType);
        if (AccessToken is not null)
            httpRequest.Headers.Authorization = new("Bearer", AccessToken);
        if (ClientId is not null)
            httpRequest.Headers.Add("Client-Id", ClientId);

        return httpRequest;
    }
}
