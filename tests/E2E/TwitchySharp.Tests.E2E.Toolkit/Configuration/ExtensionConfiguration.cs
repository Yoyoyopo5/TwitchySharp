using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Tests.E2E;

public class ExtensionConfiguration : ITestIdentity<TwitchIdentity.Extension>
{
    public required ExtensionId ExtensionId { get; set; }
    public required UserId ExtensionOwnerUserId { get; set; }
    public required ClientSecret ApiSecret { get; set; }
    public required ExtensionSecret Secret { get; set; }
    public required ExtensionVersion Version { get; set; }
    public required ExtensionBitsProductConfiguration BitsProduct { get; set; }
    public required HashSet<TestName> Tests { get; set; }
    IReadOnlySet<TestName> ITestIdentity<TwitchIdentity.Extension>.Tests => Tests;
    TwitchIdentity.Extension ITestIdentity<TwitchIdentity.Extension>.Identity => new(ExtensionOwnerUserId, ExtensionId);
}

public class ExtensionBitsProductConfiguration
{
    public required ExtensionProductSku Sku { get; set; }
}

public static class ExtensionConfigurationExtensions
{
    public static TwitchIdentity.Extension ToIdentity(this ExtensionConfiguration config)
        => new(
            OwnerId: config.ExtensionOwnerUserId,
            ExtensionId: config.ExtensionId
            );
}
