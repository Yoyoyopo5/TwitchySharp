namespace TwitchySharp.Api.Tests.E2E;

public class ExtensionConfiguration
{
    public required string ExtensionId { get; set; }
    public required string ApiSecret { get; set; }
    public required string Secret { get; set; }
    public required string Version { get; set; }
    public required ExtensionBitsProductConfiguration BitsProduct { get; set; }
}

public class ExtensionBitsProductConfiguration
{
    public required string Sku { get; set; }
}

public static class ExtensionConfigurationExtensions
{
    public static Extension ToExtension(this ExtensionConfiguration config)
        => new()
        {
            Id = new(config.ExtensionId),
            Secret = new(config.ApiSecret),
            SharedSecret = new(config.Secret),
            Version = new(config.Version),
            BitsProduct = new()
            {
                Sku = new(config.BitsProduct.Sku)
            }
        };
}
