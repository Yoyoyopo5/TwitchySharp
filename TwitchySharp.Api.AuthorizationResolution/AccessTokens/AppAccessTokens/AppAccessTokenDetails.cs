namespace TwitchySharp.Api.AuthorizationResolution;

public record AppAccessTokenDetails
    : AccessTokenDetails
{
    public required ClientIdentity Client { get; init; }
    public required AppAccessToken AccessToken { get; init; }
}
