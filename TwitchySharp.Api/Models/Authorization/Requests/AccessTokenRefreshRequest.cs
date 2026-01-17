using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Models.Authorization.Responses;

namespace TwitchySharp.Api.Models.Authorization.Requests;
/// <summary>
/// Use this request to obtain a new user access token for a previously authorized user.
/// </summary>
/// <remarks>
/// Requires a refresh token obtained when authorizing a user using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/refresh-tokens/">refresh tokens</see> for more information.
/// </remarks>
public record AccessTokenRefreshRequest
    : TwitchAuthorizationRequest<AccessTokenRefreshResponse>
{
    /// <param name="clientId">The client ID of the application that the user originally authorized.</param>
    /// <param name="clientSecret">The client secret of the application that the user originally authorized.</param>
    /// <param name="refreshToken">The refresh token for the user access token.</param>
    public AccessTokenRefreshRequest(string clientId, string clientSecret, string refreshToken)
        : base("/token")
    {
        Method = HttpMethod.Post;
        ClientId = clientId;
        Content = new FormUrlEncodedContent(
            new Dictionary<string, string>()
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "refresh_token", refreshToken },
                { "grant_type", "refresh_token" }
            }
        );
    }
}
