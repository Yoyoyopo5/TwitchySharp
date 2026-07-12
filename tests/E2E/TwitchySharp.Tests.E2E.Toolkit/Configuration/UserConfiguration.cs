using TwitchySharp.Api;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Tests.E2E;

[Wrapper<string>]
public readonly partial record struct EndpointName(string Value);

public class UserConfiguration
{
    public required UserId UserId { get; set; }
    public required UserTokenConfiguration Token { get; set; }
    public required HashSet<EndpointName> EnabledEndpoints { get; set; }
}

public class UserTokenConfiguration
{
    public required UserAccessToken AccessToken { get; set; }
    public required RefreshToken RefreshToken { get; set; }
    public required Scope[] Scopes { get; set; }
    public required DateTimeOffset ExpiresAt { get; set; }
}

public static class UserTokenConfigurationExtensions
{
    public static UserConfiguration? WithEndpointName(this IEnumerable<UserConfiguration> users, EndpointName endpoint)
        => users.FirstOrDefault(u => u.EnabledEndpoints.Contains(endpoint));

    public static AccessTokenDetails.User ToAccessTokenDetails(this UserConfiguration config, ClientId clientId)
        => new()
        {
            Identity = new TwitchIdentity.User(new(config.UserId), clientId),
            AccessToken = new(config.Token.AccessToken),
            RefreshToken = new(config.Token.RefreshToken),
            Scopes = config.Token.Scopes.Select(s => new Scope(s)).ToHashSet(),
            ExpiresAt = config.Token.ExpiresAt
        };
}
