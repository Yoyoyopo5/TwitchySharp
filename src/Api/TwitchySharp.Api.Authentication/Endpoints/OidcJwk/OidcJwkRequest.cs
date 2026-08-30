namespace TwitchySharp.Api.Authentication;
/// <summary>
/// Requests JsonWebKeys from Twitch used to validate JsonWebTokens returned in the OIDC authorization flow (as the <c>IdToken</c>).
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oidc/#validating-an-id-token">Validating an ID Token</see> for more information.
/// </remarks>
public record OidcJwkRequest
    : TwitchAuthorizationRequest<OidcJwkResponse>
{
    protected override string Path => "/keys";
    public override HttpMethod Method => HttpMethod.Get;
}
