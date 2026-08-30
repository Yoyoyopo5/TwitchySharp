using System.Collections.Immutable;

namespace TwitchySharp.Api.Authentication;
/// <summary>
/// Gets a set of OIDC claims associated with the user access token used to make the request.
/// </summary>
/// <remarks>
/// Requires a user access token including <see cref="Scope.OpenId"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#getting-claims-information-from-an-access-token">getting claims information from an access token</see> for more information.
/// </remarks>
public record UserInfoRequest
    : TwitchAuthorizationRequest<TwitchOidc>,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    protected override string Path => "/userinfo";
    public override HttpMethod Method => HttpMethod.Get;

    /// <summary>
    /// The id of the user to get claims information for.
    /// </summary>
    /// <remarks>
    /// The user must have an access token with <see cref="Scope.OpenId"/>.
    /// You may set this identity or manually configure the <see cref="AccessToken"/> to use.
    /// </remarks>
    public UserId? UserId { get; init; }

    /// <summary>
    /// The user access token of the user to get claims information for.
    /// </summary>
    /// <remarks>
    /// Requires <see cref="Scope.OpenId"/>.
    /// </remarks>
    public UserAccessToken? AccessToken { get; init; }

    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext =>
        UserId.HasValue
        ? new UserWithScopesAuthenticationContext()
        {
            Identity = new TwitchIdentity.User(UserId.Value),
            ValidScopes = ImmutableHashSet.Create(Scope.OpenId),
            BearerToken = AccessToken
        }
        : new TwitchRequestAuthenticationContext<TwitchIdentity.None>()
        {
            Identity = TwitchIdentity.None.Instance,
            BearerToken = AccessToken
        };
}
