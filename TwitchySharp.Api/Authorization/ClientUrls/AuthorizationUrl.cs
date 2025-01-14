using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Authorization.ClientUrls;
/// <summary>
/// An abstract class used to format client-side authorization urls for users of your application.
/// See <see cref="AuthorizationCodeGrantUrl"/> and <see cref="ImplicitGrantUrl"/> for implementations.
/// </summary>
public abstract class AuthorizationUrl
{
    private const string ENDPOINT = "https://id.twitch.tv/oauth2/authorize";
    public string Url { get; }
    public abstract string ResponseType { get; }

    public override string ToString()
        => Url;

    protected AuthorizationUrl(string clientId, string redirectUri, IEnumerable<Scope> scopes, string? state = null, string? nonce = null, OidcClaims? claims = null, bool forceVerify = false)
        => Url = ENDPOINT +
                new HttpQueryParameters()
                    .Add("response_type", ResponseType)
                    .Add("client_id", clientId)
                    .Add("redirect_uri", redirectUri)
                    .Add("scope", scopes.FormatScopes())
                    .Add("force_verify", forceVerify.ToString())
                    .Add("state", state)
                    .Add("nonce", nonce)
                    .Add("claims", claims?.JsonEncode());
}
