using TwitchySharp.Api.AuthorizationResolution;

namespace TwitchySharp.Api.Tests.E2E;

public class UserTokenConfiguration
{
    public required UserIdentityConfiguration Identity { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required string[] Scopes { get; set; }
    public required string ExpiresAt { get; set; }
}

public class UserIdentityConfiguration
{
    public required string UserId { get; set; }
    public required string ClientId { get; set; }
}

public static class UserTokenConfigurationExtensions
{
    public static AccessTokenDetails.User ToAccessTokenDetails(this UserTokenConfiguration config)
        => new()
        {
            Identity = new TwitchIdentity.User(new(config.Identity.UserId), new(config.Identity.ClientId)),
            AccessToken = new(config.AccessToken),
            RefreshToken = new(config.RefreshToken),
            Scopes = config.Scopes.Select(s => new Scope(s)).ToHashSet(),
            ExpiresAt = DateTimeOffset.Parse(config.ExpiresAt)
        };
}
