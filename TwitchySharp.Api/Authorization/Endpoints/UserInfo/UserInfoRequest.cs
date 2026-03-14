using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Gets a set of OIDC claims associated with the user access token used to make the request.
/// </summary>
/// <remarks>
/// Requires a user access token including <see cref="Scope.OpenId"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">getting claims information from an access token</see> for more information.
/// </remarks>
public record UserInfoRequest
    : TwitchAuthorizationRequest<TwitchOidc>, IAuthorizedTwitchRequest
{
    protected override string Path => "/userinfo";
    public override HttpMethod Method => HttpMethod.Get;

    /// <summary>
    /// The user access token of the user to get claims information for.
    /// </summary>
    /// <remarks>
    /// Requires <see cref="Scope.OpenId"/>.
    /// </remarks>
    public required UserAccessToken AccessToken { get; init; }

    public TwitchRequestAuthorizationContext AuthorizationContext => new()
    {
        Identity = TwitchIdentity.None.Instance,
        ValidScopes = ImmutableHashSet.Create(Scope.OpenId),
        AccessToken = AccessToken
    };
}
