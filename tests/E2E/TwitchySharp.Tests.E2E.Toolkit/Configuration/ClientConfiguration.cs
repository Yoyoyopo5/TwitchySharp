using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Tests.E2E;

public class ClientConfiguration : ITestIdentity<TwitchIdentity.Client>
{
    public required ClientId ClientId { get; set; }
    public required ClientSecret ClientSecret { get; set; }
    public required HashSet<TestName> Tests { get; set; }
    IReadOnlySet<TestName> ITestIdentity<TwitchIdentity.Client>.Tests => Tests;
    TwitchIdentity.Client ITestIdentity<TwitchIdentity.Client>.Identity => new(ClientId);
}

public static class ClientConfigurationExtensions
{
    public static TwitchIdentity.Client ToIdentity(this ClientConfiguration config)
        => new(config.ClientId);
}
