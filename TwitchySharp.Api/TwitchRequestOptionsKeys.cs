using System.Net.Http;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;

/// <summary>
/// Contains static definitions for options contained in the <see cref="HttpRequestMessage.Options"/>.
/// </summary>
public static class TwitchRequestOptionsKeys
{
    /// <summary>
    /// Metadata used to set the correct <see cref="TwitchAuthorizationRequestOptions"/>.
    /// </summary>
    internal static HttpRequestOptionsKey<TwitchRequest> TwitchRequest { get; } = new("twitch-request");
    /// <summary>
    /// Information used to set the headers required for authorization.
    /// </summary>
    public static HttpRequestOptionsKey<TwitchAuthorizationRequestOptions> Authorization { get; } = new("twitch-authorization");
}
