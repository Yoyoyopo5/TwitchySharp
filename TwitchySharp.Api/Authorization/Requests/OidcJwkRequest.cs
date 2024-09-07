using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TwitchySharp.Api.Authorization.Responses;

namespace TwitchySharp.Api.Authorization.Requests;
/// <summary>
/// Requests JsonWebKeys from Twitch used to validate JsonWebTokens returned in the OIDC authorization flow (as the IdToken).
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#validating-an-id-token">validating an ID token</see> for more information.
/// </summary>
public class OidcJwkRequest()
    : AuthorizationApiRequest<OidcJwkResponse>("keys")
{
    public override string? Data => null;
    public override HttpMethod Method => HttpMethod.Get;
    public override string? ContentType => null;
}
