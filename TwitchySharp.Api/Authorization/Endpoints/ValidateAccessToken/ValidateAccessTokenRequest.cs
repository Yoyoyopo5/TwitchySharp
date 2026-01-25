using System.Collections.Generic;
using System.Linq;
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
    /// The identity for this request. Validation does not require a specific identity context.
    /// </summary>
    public TwitchApiIdentity Identity { get; init; } = TwitchApiIdentity.None;

    /// <summary>
    /// No specific scopes are required for token validation.
    /// </summary>
    public IEnumerable<Scope> ValidScopes => Enumerable.Empty<Scope>();

    /// <summary>
    /// The access token used for authorization. Returns the <see cref="AccessToken"/> to validate.
    /// </summary>
    public AccessToken? OverrideAccessToken => AccessToken;

    /// <inheritdoc/>
    public IRequireAuthorization WithClientFallback(ClientIdentity? client)
        => this with { Identity = Identity.WithFallbackClient(client) };
}
