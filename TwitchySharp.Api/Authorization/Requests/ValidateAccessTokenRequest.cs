using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Checks if a given user access token is currently valid.
/// Please note that Twitch requires applications validate user access tokens every hour.
/// See <see href="https://dev.twitch.tv/docs/authentication/validate-tokens/">validate tokens</see> for more information.
/// </summary>
public class ValidateAccessTokenRequest
    : AuthorizationApiRequest<ValidateAccessTokenResponse>
{
    public override string? Data => null;
    public override HttpMethod Method => HttpMethod.Get;
    public override string? ContentType => null;

    /// <param name="accessToken">The user access token to validate.</param>
    public ValidateAccessTokenRequest(string accessToken)
        : base("validate")
    {
        AccessToken = accessToken;
    }
}
