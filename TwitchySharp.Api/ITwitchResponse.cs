using System.Net;

namespace TwitchySharp.Api;
/// <summary>
/// A general Twitch API response with basic information.
/// </summary>
public interface ITwitchResponse
{
    /// <summary>
    /// The request resulting in this response.
    /// </summary>
    ITwitchRequest Request { get; }
    /// <summary>
    /// The HTTP status code of the response.
    /// </summary>
    HttpStatusCode StatusCode { get; }
    /// <summary>
    /// The Twitch rate limit details included with the response, if any.
    /// </summary>
    TwitchRateLimitDetails? RateLimitDetails { get; }
}

/// <summary>
/// A Twitch API response with strongly-typed response content.
/// </summary>
/// <typeparam name="TResponseContent">
/// The type of the content. 
/// Note that this should be set by the request type.
/// </typeparam>
public interface ITwitchResponse<TResponseContent> : ITwitchResponse
{
    /// <summary>
    /// The strongly-typed content in the API response.
    /// </summary>
    TResponseContent Content { get; }
}
