using System;
using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization.ClientUrls;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get a user access token and refresh token.
/// </summary>
/// <remarks>
/// This flow is meant for apps that use a server, can securely store a client secret, and can make server-to-server requests to the Twitch API.
/// Requires a code obtained when a user authorizes your application via an <see cref="AuthorizationCodeGrantUrl"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">Authorization Code Grant Flow</see> for more information.
/// </remarks>
public record AuthorizationCodeRequest
    : TwitchAuthorizationRequest<AuthorizationCodeResponse>
{
    protected override string Path => "/token";
    public override HttpMethod Method => HttpMethod.Post;
    public override HttpContent? Content
        => new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "code", Code },
            { "grant_type", "authorization_code" },
            { "redirect_uri", RedirectUri.Value }
        });

    /// <summary>
    /// The client id of the application.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The client secret of the application.
    /// </summary>
    public required ClientSecret ClientSecret { get; init; }
    /// <summary>
    /// The code query parameter obtained from the redirect of an <see cref="AuthorizationCodeGrantUrl"/>.
    /// </summary>
    public required string Code { get; init; }
    /// <summary>
    /// The redirect URI of the application (this is registered via the Twitch developer console).
    /// This should be the URI that the <see cref="AuthorizationCodeGrantUrl"/> redirected to.
    /// </summary>
    public required RedirectUri RedirectUri { get; init; }
}
