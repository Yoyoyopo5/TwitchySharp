using System;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix;
/// <summary>
/// Used to create a request to a Twitch Helix API endpoint.
/// </summary>
/// <typeparam name="TResponseContent">The response content type of the request.</typeparam>
public record TwitchHelixRequest<TResponseContent>
    : TwitchRequest<TResponseContent>
{
    private const string TWITCH_HELIX_HOST = "api.twitch.tv";
    private const string HELIX_BASE_PATH = "/helix";
    /// <summary>
    /// <inheritdoc cref="TwitchHelixRequest{TResponse}"/>
    /// </summary>
    /// <param name="path">The path of the endpoint (after <c>/helix</c>).</param>
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An access token used to authorize the request. This is used in the <c>Authorization</c> header.</param>
    public TwitchHelixRequest(string path, ClientId clientId, AccessToken accessToken, HttpQueryParameters? queryParams = null)
    {
        ClientId = clientId;
        AccessToken = accessToken;
        RequestUri = new UriBuilder
        {
            Scheme = "https",
            Host = TWITCH_HELIX_HOST,
            Path = HELIX_BASE_PATH + path,
            Query = queryParams?.ToString()
        }.Uri;
    }
}
