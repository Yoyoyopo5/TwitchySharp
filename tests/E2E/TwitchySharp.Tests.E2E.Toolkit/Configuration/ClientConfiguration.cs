using TwitchySharp.Api;

namespace TwitchySharp.Tests.E2E;

public class ClientConfiguration : ITestIdentity
{
    public required ClientId ClientId { get; set; }
    public required ClientSecret ClientSecret { get; set; }
    public required HashSet<TestName> Tests { get; set; }
    IReadOnlySet<TestName> ITestIdentity.Tests => Tests;
}

public static class ClientConfigurationExtensions
{
    public static TwitchIdentity.Client ToIdentity(this ClientConfiguration config)
        => new(config.ClientId);
}
