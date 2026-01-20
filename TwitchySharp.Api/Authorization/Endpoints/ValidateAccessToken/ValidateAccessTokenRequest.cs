using System.Net.Http;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Checks if a given user access token is currently valid.
/// </summary>
/// <remarks>
/// Please note that Twitch requires applications validate user access tokens every hour.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/validate-tokens/">Validate Tokens</see> for more information.
/// </remarks>
public record ValidateAccessTokenRequest
    : TwitchAuthorizationRequest<ValidateAccessTokenResponse>
{
    /// <param name="accessToken">The user access token to validate.</param>
    public ValidateAccessTokenRequest(UserAccessToken accessToken)
        : base("/validate")
    {
        Method = HttpMethod.Get;
        AccessToken = accessToken;
    }
}
