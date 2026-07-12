using TwitchySharp.Api;

namespace TwitchySharp.Tests.E2E;

public class ExtensionConfiguration
{
    public required ExtensionId ExtensionId { get; set; }
    public required ClientSecret ApiSecret { get; set; }
    public required ExtensionSecret Secret { get; set; }
    public required ExtensionVersion Version { get; set; }
    public required ExtensionBitsProductConfiguration BitsProduct { get; set; }
}

public class ExtensionBitsProductConfiguration
{
    public required ExtensionProductSku Sku { get; set; }
}
