namespace TwitchySharp.Api.AuthorizationResolution;

public record AppAccessTokenDetails
    : AccessTokenDetails<ClientIdentity, AppAccessToken>
{
    public override required ClientIdentity Identity { get; init; }
    public override required AppAccessToken AccessToken { get; init; }
}
