using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Tests.E2E;

public class UserConfiguration : ITestIdentity<TwitchIdentity.User>
{
    public required UserId UserId { get; set; }
    public required UserTokenConfiguration Token { get; set; }
    public required HashSet<TestName> Tests { get; set; }
    IReadOnlySet<TestName> ITestIdentity<TwitchIdentity.User>.Tests => Tests;
    TwitchIdentity.User ITestIdentity<TwitchIdentity.User>.Identity => new(UserId, Token.ClientId);
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
