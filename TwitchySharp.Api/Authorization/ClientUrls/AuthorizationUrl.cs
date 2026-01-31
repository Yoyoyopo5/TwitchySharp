using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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

    /// <summary>
    /// The URI host.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>id.twitch.tv</c>.
    /// Only override if you have a special use case (e.g. testing).
    /// </remarks>
    public string Host { get; init; } = DEFAULT_HOST;
    /// <summary>
    /// The URI path.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>/oauth2/authorize</c>.
    /// Only override if you have a special use case (e.g. testing).
    /// </remarks>
    public string Path { get; init; } = DEFAULT_PATH;
    /// <summary>
    /// The URI scheme.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>https</c>.
    /// Only override if you have a special use case (e.g. testing).
    /// </remarks>
    public string Scheme { get; init; } = DEFAULT_SCHEME;

    /// <summary>
    /// The client id of the Twitch API application.
    /// </summary>
    /// <remarks>
    /// This can be obtained from the <see href="https://dev.twitch.tv/console">Twitch Developer Console</see>.
    /// <br/>
    /// See <see href="https://dev.twitch.tv/docs/api/get-started">Get Started</see> for more information.
    /// </remarks>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The redirect URI where the authorizing user will be sent along with their authorization code.
    /// </summary>
    /// <remarks>
    /// This must be a redirect URI registered for your application (the <see cref="ClientId"/>) in the <see href="https://dev.twitch.tv/console">Twitch Developer Console</see>.
    /// </remarks>
    public required Uri RedirectUri { get; init; }
    protected abstract ImmutableHashSet<TwitchAuthorizationResponseType> ResponseTypes { get; init; }
    /// <summary>
    /// The user access token <see href="https://dev.twitch.tv/docs/authentication/scopes/">Scopes</see> to get authorization for.
    /// </summary>
    /// <remarks>
    /// These will be presented to the authorizing user during the authorization process.
    /// </remarks>
    public required IEnumerable<Scope> Scopes { get; init; }
    /// <summary>
    /// An arbitrary string value that will be attached as a query parameter (<see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see>) or fragment parameter (<see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#implicit-grant-flow">implicit grant flow</see>) to the redirect URI by Twitch after authorization.
    /// </summary>
    /// <remarks>
    /// Although optional, you are strongly encouraged to pass a state string to help prevent Cross-Site Request Forgery (CSRF) attacks.  
    /// If this string doesn’t match the state string that you passed, ignore the response. 
    /// The state string should be randomly generated and unique for each OAuth request. 
    /// </remarks>
    public string? State { get; init; }
    /// <summary>
    /// An arbitrary string added to the id token's list of claims if you request an <see href="https://openid.net/developers/how-connect-works/">OpenID Connect</see> ID token.
    /// </summary>
    /// <remarks>
    /// Although optional, you are strongly encouraged to pass a nonce string to help prevent Cross-Site Request Forgery (CSRF) attacks. 
    /// If this string doesn’t match the nonce string that you passed, ignore the response. 
    /// The nonce string should be randomly generated and unique for each OAuth request.
    /// </remarks>
    public string? Nonce { get; init; }
    /// <summary>
    /// The claims to get in the id token, if requesting an <see href="https://openid.net/developers/how-connect-works/">OpenID Connect</see> ID token.
    /// </summary>
    /// <remarks>
    /// For information about claims, see <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#requesting-claims">Requesting Claims</see>.
    /// </remarks>
    public OidcClaims? Claims { get; init; }
    /// <summary>
    /// Set to <see langword="true"/> to always prompt the authorizing user to re-authorize your application on the Twitch website, even if they have already done so in the past.
    /// </summary>
    /// <remarks>
    /// If <see langword="false"/> or <see langword="null"/> and the user has already authorized your application with the requested scopes, they will not be prompted to do so again,
    /// and they will be automatically redirected to the <see cref="RedirectUri"/>.
    /// <br/>
    /// Defaults to <see langword="null"/>.
    /// </remarks>
    public bool? ForceVerify { get; init; }

    /// <summary>
    /// The URI to send the authorizing user to.
    /// </summary>
    /// <remarks>
    /// This is generated once per call.
    /// </remarks>
    public Uri Uri => new UriBuilder
    {
        Scheme = Scheme,
        Host = Host,
        Path = Path,
        Query = new HttpQueryParameters()
            .Add("response_type", string.Join("+", ResponseTypes.Select(x => x.Value)))
            .Add("client_id", ClientId)
            .Add("redirect_uri", RedirectUri.ToString())
            .Add("scope", Scopes.FormatScopes())
            .Add("force_verify", ForceVerify?.ToString())
            .Add("state", State)
            .Add("nonce", Nonce)
            .Add("claims", Claims?.JsonEncode())
            .ToString()
    }.Uri;
}
