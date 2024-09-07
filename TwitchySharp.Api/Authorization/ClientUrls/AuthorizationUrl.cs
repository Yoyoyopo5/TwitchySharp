using System;
using System.Collections.Generic;
using System.Text;

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

    public AuthorizationUrl(string clientId, string redirectUri, IEnumerable<Scope> scopes, string? state = null, string? nonce = null, OidcClaims? claims = null, bool forceVerify = false)
    {
        string url = $"https://id.twitch.tv/oauth2/authorize" +
        $"?response_type={ResponseType}" +
        $"&client_id={clientId}" +
        $"&redirect_uri={redirectUri}" +
        $"&scope={scopes.FormatScopes()}" +
        $"force_verify={forceVerify}";

        if (state is not null)
            url += $"&state={state}";
        if (nonce is not null)
            url += $"&nonce={nonce}";
        if (claims is not null)
            url += $"&claims={claims.JsonEncode()}";

        Url = url;
    }
}
