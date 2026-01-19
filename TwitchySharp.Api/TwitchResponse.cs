using System.Net;

namespace TwitchySharp.Api;
/// <summary>
/// A general Twitch API response with no content.
/// </summary>
public record TwitchResponse : ITwitchResponse
{
    public TwitchRateLimitDetails? RateLimitDetails { get; init; }
    public required ITwitchRequest Request { get; init; }
    public required HttpStatusCode StatusCode { get; init; }
}

/// <summary>
/// A general Twitch API response with strongly-typed content.
/// </summary>
/// <typeparam name="TResponseContent">The response content type.</typeparam>
public record TwitchResponse<TResponseContent> : TwitchResponse, ITwitchResponse<TResponseContent>
{
    public required TResponseContent Content { get; init; }
}
