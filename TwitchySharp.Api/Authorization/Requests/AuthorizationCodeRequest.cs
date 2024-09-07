using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Authorization.Requests;
/// <summary>
/// 
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <param name="clientId"></param>
/// <param name="clientSecret"></param>
/// <param name="code"></param>
/// <param name="redirectUri"></param>
public class AuthorizationCodeRequest<TResponse>(string clientId, string clientSecret, string code, string redirectUri) // Think of a better name?
    : AuthorizationApiRequest<TResponse>("/token")
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
