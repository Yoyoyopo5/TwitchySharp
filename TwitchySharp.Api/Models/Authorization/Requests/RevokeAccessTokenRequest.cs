using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TwitchySharp.Api.Models.Authorization;
using TwitchySharp.Api.Models.Authorization.Responses;

namespace TwitchySharp.Api.Models.Authorization.Requests;
/// <summary>
/// Revokes a valid user access token so that it is no longer valid.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/authentication/revoke-tokens/">revoke tokens</see> for more information.
/// </remarks>
public record RevokeAccessTokenRequest
    : TwitchAuthorizationRequest<RevokeAccessTokenResponse>
{
    /// <param name="clientId">The client id of the application that the <paramref name="accessToken"/> was created under.</param>
    /// <param name="accessToken">The access token to revoke.</param>
    public RevokeAccessTokenRequest(string clientId, string accessToken)
        : base("/revoke")
    {
        Method = HttpMethod.Post;
        ClientId = clientId;
        AccessToken = accessToken;
        Content = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            { "client_id", clientId },
            { "token", accessToken }
        });
    }
}
