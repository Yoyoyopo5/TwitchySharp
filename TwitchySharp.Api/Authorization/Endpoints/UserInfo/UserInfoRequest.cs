using System.Collections.Generic;
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
    : TwitchAuthorizationRequest<TwitchOidc>, IRequireAuthorization
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

    /// <summary>
    /// The identity for this request.
    /// </summary>
    /// <remarks>
    /// UserInfo does not require a specific identity context.
    /// The <see cref="AccessToken"/> will be used in the Authorization header.
    /// </remarks>
    public TwitchApiIdentity Identity { get; init; } = TwitchApiIdentity.None;

    /// <summary>
    /// Requires <see cref="Scope.OpenId"/>.
    /// </summary>
    public IEnumerable<Scope> ValidScopes => [ Scope.OpenId ];

    /// <summary>
    /// The access token used for authorization. Returns the <see cref="AccessToken"/> property.
    /// </summary>
    public AccessToken? OverrideAccessToken => AccessToken;
}
