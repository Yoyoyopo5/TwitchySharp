using System.Net.Http;

namespace TwitchySharp.Api.Core;

internal record TwitchAuthorizationRequestOptions
{
    public string? ClientId { get; init; }
    public string? AccessToken { get; init; }
}

internal static class TwitchRequestOptionsKeys
{
    public static HttpRequestOptionsKey<TwitchAuthorizationRequestOptions> Authorization { get; } = new("twitch-authorization");
}
