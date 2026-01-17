using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;
/// <summary>
/// A <see cref="DelegatingHandler"/> that adds the <c>Client-Id</c> and Bearer Authorization headers
/// required by many Twitch API endpoints via <see cref="HttpRequestMessage.Options"/>.
/// </summary>
public class TwitchAuthorizationHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct = default)
    {
        request.AddTwitchAuthorizationHeaders();
        return base.SendAsync(request, ct);
    }
}

/// <summary>
/// Used to set <see cref="HttpRequestMessage"/> Options that can later be converted into headers.
/// </summary>
public record TwitchAuthorizationRequestOptions
{
    /// <summary>
    /// The value that should be included in the <c>Client-Id</c> header.
    /// </summary>
    public string? ClientId { get; init; }
    /// <summary>
    /// The value that should be set as the bearer authorization.
    /// </summary>
    public string? AccessToken { get; init; }
}

/// <summary>
/// Contains static definitions for options contained in the <see cref="HttpRequestMessage.Options"/>.
/// </summary>
public static class TwitchRequestOptionsKeys
{
    /// <summary>
    /// The authorization options, including client id and access token.
    /// </summary>
    public static HttpRequestOptionsKey<TwitchAuthorizationRequestOptions> Authorization { get; } = new("twitch-authorization");
}
