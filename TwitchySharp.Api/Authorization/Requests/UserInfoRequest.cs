using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Gets a set of OIDC claims associated with the user access token used to make the request.
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">getting claims information from an access token</see> for more information.
/// Requires <see cref="Scope.OpenId"/>.
/// </summary>
public class UserInfoRequest
    : AuthorizationApiRequest<TwitchOidc>
{
    public UserInfoRequest(string accessToken) 
        : base("/userinfo")
    {
        AccessToken = accessToken;
    }
    public override string? Data => null;
    public override HttpMethod Method => HttpMethod.Get;
    public override string? ContentType => "application/json";
}
