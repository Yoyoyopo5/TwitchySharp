using System.Net.Http;
using TwitchySharp.Shared.Models;

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
    /// The id of the user to validate an access token for.
    /// </summary>
    /// <remarks>
    /// You may set this or configure the <see cref="AccessToken"/> property manually 
    /// (if not using authorization resolution).
    /// </remarks>
    public UserId? UserId { get; init; }

    /// <summary>
    /// The user access token to validate.
    /// </summary>
    /// <remarks>
    /// This token is sent as the Bearer authorization header.
    /// </remarks>
    public UserAccessToken? AccessToken { get; init; }

    public TwitchRequestAuthorizationContext AuthorizationContext => new()
    {
        Identity = UserId.HasValue ? new TwitchIdentity.User(UserId.Value) : TwitchIdentity.None.Instance,
        AccessToken = AccessToken
    };
}
