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

    /// <summary>
    /// The identity for this request.
    /// </summary>
    /// <remarks>
    /// Validation does not require a specific identity context.
    /// The <see cref="AccessToken"/> will be used in the Authorization header.
    /// </remarks>
    public TwitchApiIdentity Identity { get; init; } = TwitchApiIdentity.None;

    /// <summary>
    /// No specific scopes are required for token validation.
    /// </summary>
    public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;

    /// <summary>
    /// The access token used for authorization. Returns the <see cref="AccessToken"/> to validate.
    /// </summary>
    public AccessToken? OverrideAccessToken => AccessToken;
}
