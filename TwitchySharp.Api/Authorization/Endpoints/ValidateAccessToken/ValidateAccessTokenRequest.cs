using System.Collections.Generic;
using System.Collections.Immutable;
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
    : TwitchAuthorizationRequest<ValidateAccessTokenResponse>, IAuthorizedTwitchRequest
{
    public override HttpMethod Method => HttpMethod.Get;
    protected override string Path => "/validate";

    /// <summary>
    /// The user access token to validate.
    /// </summary>
    /// <remarks>
    /// This token is sent as the Bearer authorization header.
    /// </remarks>
    public required UserAccessToken AccessToken { get; init; }

    public TwitchRequestAuthorizationContext AuthorizationContext => new()
    {
        Identity = TwitchIdentity.None.Instance,
        AccessToken = AccessToken
    };
}
