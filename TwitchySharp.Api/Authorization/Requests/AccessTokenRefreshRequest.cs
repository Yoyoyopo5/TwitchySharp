using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Use this request to obtain a new user access token for a previously authorized user.
/// Requires a refresh token obtained when authorizing a user using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see>.
/// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens/">refresh tokens</see> for more information.
/// </summary>
/// <param name="clientId">The client ID of the application that the user originally authorized.</param>
/// <param name="clientSecret">The client secret of the application that the user originally authorized.</param>
/// <param name="refreshToken">The refresh token for the user access token.</param>
public class AccessTokenRefreshRequest(string clientId, string clientSecret, string refreshToken)
    : AuthorizationApiRequest<AccessTokenRefreshResponse>("token")
{
    /// <summary>
    /// The client ID of the application that the user originally authorized.
    /// </summary>
    public new string ClientId { get; } = clientId;
    /// <summary>
    /// The client secret of the application that the user originally authorized.
    /// </summary>
    public string ClientSecret { get; } = clientSecret;
    /// <summary>
    /// The refresh token for the user access token. This is included when using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see>.
    /// </summary>
    public string RefreshToken { get; } = refreshToken;
    public override string? Data => $"client_id={ClientId}&client_secret={ClientSecret}&refresh_token={RefreshToken}&grant_type=refresh_token";
    public override HttpMethod Method => HttpMethod.Post;
}
