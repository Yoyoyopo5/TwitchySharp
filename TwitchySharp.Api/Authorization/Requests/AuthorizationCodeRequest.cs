using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TwitchySharp.Api.Authorization.ClientUrls;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get a user access token and refresh token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">Authorization Code Grant Flow</see> for more information.
/// </summary>
/// <remarks>
/// This flow is meant for apps that use a server, can securely store a client secret, and can make server-to-server requests to the Twitch API.
/// Requires a code obtained when a user authorizes your application via an <see cref="AuthorizationCodeGrantUrl"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="clientSecret">The client secret of the application.</param>
/// <param name="code">The code query parameter obtained from the redirect of an <see cref="AuthorizationCodeGrantUrl"/>.</param>
/// <param name="redirectUri">
/// The redirect URI of the application (this is registered via the Twitch developer console).
/// This should be the URI that the <see cref="AuthorizationCodeGrantUrl"/> redirected to.
/// </param>
public class AuthorizationCodeRequest(string clientId, string clientSecret, string code, string redirectUri) // Think of a better name?
    : AuthorizationApiRequest<AuthorizationCodeResponse>("/token")
{
    /// <summary>
    /// The client id of the application that will be used in the request.
    /// </summary>
    public new string ClientId { get; } = clientId;
    /// <summary>
    /// The client secret of the application that will be used in the request.
    /// </summary>
    public string ClientSecret { get; } = clientSecret;
    /// <summary>
    /// The code returned in the redirect url fragment when the user authenticated the app through Twitch.
    /// </summary>
    public string Code { get; } = code;
    /// <summary>
    /// The app's registered redirect uri.
    /// </summary>
    public string RedirectUri { get; } = redirectUri;

    public override string? Data => $"client_id={ClientId}&client_secret={ClientSecret}&code={Code}&grant_type=authorization_code&redirect_uri={RedirectUri}";
    public override HttpMethod Method => HttpMethod.Post;
    public override string ContentType => "application/x-www-form-urlencoded";
}
