using System.Net.Http;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Requests JsonWebKeys from Twitch used to validate JsonWebTokens returned in the OIDC authorization flow (as the <c>IdToken</c>).
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#validating-an-id-token">validating an ID token</see> for more information.
/// </remarks>
public record OidcJwkRequest
    : TwitchAuthorizationRequest<OidcJwkResponse>
{
    public OidcJwkRequest()
        : base("/keys")
    {
        Method = HttpMethod.Get;
    }
}
