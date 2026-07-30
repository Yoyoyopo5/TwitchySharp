using TwitchySharp.Api;

namespace TwitchySharp.Tests.E2E;

public class UserConfiguration : ITestIdentity
{
    public required UserId UserId { get; set; }
    public required UserTokenConfiguration Token { get; set; }
    public required HashSet<TestName> Tests { get; set; }
    IReadOnlySet<TestName> ITestIdentity.Tests => Tests;
}

public class UserTokenConfiguration
{
    public required ClientId ClientId { get; set; }
    public required UserAccessToken AccessToken { get; set; }
    public required RefreshToken RefreshToken { get; set; }
    public required Scope[] Scopes { get; set; }
    public required DateTimeOffset ExpiresAt { get; set; }
}

public static class UserTokenConfigurationExtensions
{
    public static AccessTokenDetails.User ToAccessTokenDetails(this UserConfiguration config)
        => new()
        {
            Identity = new TwitchIdentity.User(new(config.UserId), config.Token.ClientId),
            AccessToken = new(config.Token.AccessToken),
            RefreshToken = new(config.Token.RefreshToken),
            Scopes = config.Token.Scopes.Select(s => new Scope(s)).ToHashSet(),
            ExpiresAt = config.Token.ExpiresAt
        };
}
