using System.Net;

namespace TwitchySharp.Api;
/// <summary>
/// A general Twitch API response with no content.
/// </summary>
public record TwitchResponse
{
    /// <summary>
    /// The request resulting in this response.
    /// </summary>
    public TwitchRateLimitDetails? RateLimitDetails { get; init; }
    /// <summary>
    /// The request resulting in this response.
    /// </summary>
    public required TwitchRequest Request { get; init; }
    /// <summary>
    /// The HTTP status code of the response.
    /// </summary>
    public required HttpStatusCode StatusCode { get; init; }
}

/// <summary>
/// A general Twitch API response with strongly-typed content.
/// </summary>
/// <typeparam name="TResponseContent">The response content type.</typeparam>
public record TwitchResponse<TResponseContent> : TwitchResponse
{
    public required TResponseContent Content { get; init; }
}
