namespace TwitchySharp.Api.AuthorizationResolution;

public record ExtensionJwtDetails
    : AccessTokenDetails<ExtensionIdentity, ExtensionJsonWebToken>
{
    public override required ExtensionIdentity Identity { get; init; }
    public override required ExtensionJsonWebToken AccessToken { get; init; }
}
