using System.Net.Http;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Gets a set of OIDC claims associated with the user access token used to make the request
/// </summary>
/// <remarks>
/// Requires a user access token including <see cref="Scope.OpenId"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">getting claims information from an access token</see> for more information.
/// </remarks>
public record UserInfoRequest
    : TwitchAuthorizationRequest<TwitchOidc>
{
    /// <param name="accessToken">
    /// The user access token of the user to get claims information for.
    /// Requires <see cref="Scope.OpenId"/>.
    /// </param>
    public UserInfoRequest(UserAccessToken accessToken)
        : base("/userinfo")
    {
        Method = HttpMethod.Get;
        AccessToken = accessToken;
    }
}
