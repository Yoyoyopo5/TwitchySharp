using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Revokes a valid user access token so that it is no longer valid.
/// See <see href="https://dev.twitch.tv/docs/authentication/revoke-tokens/">revoke tokens</see> for more information.
/// </summary>
/// <param name="clientId"></param>
/// <param name="accessToken"></param>
public class RevokeAccessTokenRequest(string clientId, string accessToken)
    : AuthorizationApiRequest<RevokeAccessTokenResponse>("/revoke")
{
    /// <summary>
    /// The client ID of the application that the access token is authorizing.
    /// </summary>
    public new string ClientId { get; } = clientId;
    /// <summary>
    /// The user access token to revoke.
    /// </summary>
    public new string AccessToken { get; } = accessToken;

    public override string? Data => $"client_id={ClientId}&token={AccessToken}";
    public override HttpMethod Method => HttpMethod.Post;
}
