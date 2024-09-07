using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TwitchySharp.Api.Authorization.ClientUrls;
/// <summary>
/// Encodes a url used to authenticate a user via the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#implicit-grant-flow">implicit grant flow</see>.
/// </summary>
/// <param name="clientId">The client id of the Twitch application.</param>
/// <param name="redirectUri">The url where the client will be sent after authorizing the application. This must also be registered in the extension settings on the <see href="https://dev.twitch.tv/">Twitch developer website</see>.</param>
/// <param name="scopes">The list of authorization <see cref="Scope"/>s that will be requested from the authorizing user.</param>
/// <param name="state">An arbitrary string that will be returned in the fragment of the redirect uri. Use this to prevent Cross-Site Request Forgery attacks.</param>
/// <param name="responseType">The token(s) that should be included in the response.</param>
/// <param name="nonce">An arbitrary string that will be included in the claims of an OpenID request. Use this to prevent Cross-Site Request Forgery attacks.</param>
/// <param name="claims">A collection of <see cref="OidcClaim"/>s that will be requested in addition to the default claims. See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#requesting-claims">requesting claims</see> for more info.</param>
/// <param name="forceVerify">If set to <see langword="true"/>, this will force the authorizing user to click an authorization button even if they have authorized this app in the past.</param>
public class ImplicitGrantUrl(string clientId, string redirectUri, IEnumerable<Scope> scopes, string? state = null, OidcResponseType responseType = OidcResponseType.Token, OidcClaims? claims = null, string? nonce = null, bool forceVerify = false)
    : AuthorizationUrl(clientId, redirectUri, scopes, state, nonce, claims, forceVerify)
{
    public override string ResponseType => $"{responseType.ToResponseType()}";
}
