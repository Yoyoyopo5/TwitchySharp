using System;
using System.Collections.Generic;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Authorization.ClientUrls;
/// <summary>
/// An abstract class used to format client-side authorization urls for users of your application.
/// </summary>
/// <remarks>
/// See <see cref="AuthorizationCodeGrantUrl"/> and <see cref="ImplicitGrantUrl"/> for implementations.
/// </remarks>
public abstract record AuthorizationUrl
{
    private const string DEFAULT_SCHEME = "https";
    private const string DEFAULT_HOST = "id.twitch.tv";
    private const string DEFAULT_PATH = "/oauth2/authorize";
    public Uri Uri { get; }
    public override string ToString()
        => Uri.ToString();

    protected AuthorizationUrl(
        ClientId clientId,
        Uri redirectUri,
        string repsonseType,
        IEnumerable<Scope> scopes,
        string? state = null,
        string? nonce = null,
        OidcClaims? claims = null,
        bool forceVerify = false
        )
    {
        Uri = new UriBuilder
        {
            Scheme = DEFAULT_SCHEME,
            Host = DEFAULT_HOST,
            Path = DEFAULT_PATH,
            Query = new HttpQueryParameters()
                    .Add("response_type", repsonseType)
                    .Add("client_id", clientId)
                    .Add("redirect_uri", redirectUri.ToString())
                    .Add("scope", scopes.FormatScopes())
                    .Add("force_verify", forceVerify.ToString())
                    .Add("state", state)
                    .Add("nonce", nonce)
                    .Add("claims", claims?.JsonEncode())
                    .ToString()
        }.Uri;
    }
}
