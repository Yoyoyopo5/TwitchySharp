using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization.ClientUrls;

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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="clientSecret">The client secret of the application.</param>
    /// <param name="code">The code query parameter obtained from the redirect of an <see cref="AuthorizationCodeGrantUrl"/>.</param>
    /// <param name="redirectUri">
    /// The redirect URI of the application (this is registered via the Twitch developer console).
    /// This should be the URI that the <see cref="AuthorizationCodeGrantUrl"/> redirected to.
    /// </param>
    public AuthorizationCodeRequest(string clientId, string clientSecret, string code, string redirectUri)
        : base("/token")
    {
        Method = HttpMethod.Post;
        ClientId = clientId;
        Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "code", code },
            { "grant_type", "authorization_code" },
            { "redirect_uri", redirectUri }
        });
    }
}
