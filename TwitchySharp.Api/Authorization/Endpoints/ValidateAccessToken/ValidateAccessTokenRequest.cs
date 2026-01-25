using System.Collections.Generic;
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
    : TwitchAuthorizationRequest<ValidateAccessTokenResponse>, IRequireAuthorization
{
    /// <param name="accessToken">The user access token to validate.</param>
    public ValidateAccessTokenRequest(UserAccessToken accessToken)
        : base("/validate")
    {
        Method = HttpMethod.Get;
        AccessToken = accessToken;
    }

    public override HttpMethod Method => HttpMethod.Get;
    protected override string Path => "/validate";

    /// <summary>
    /// The user access token to validate.
    /// </summary>
    public required UserAccessToken AccessToken { get; init; }

    public TwitchApiIdentity Identity => new UserIdentity();

    public IEnumerable<Scope> ValidScopes => throw new System.NotImplementedException();
}
