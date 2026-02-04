namespace TwitchySharp.Api.AuthorizationResolution;

public record ExtensionJwtDetails
    : AccessTokenDetails
{
    public required ExtensionIdentity Extension { get; init; }
    public required ExtensionJsonWebToken JsonWebToken { get; init; }
}
