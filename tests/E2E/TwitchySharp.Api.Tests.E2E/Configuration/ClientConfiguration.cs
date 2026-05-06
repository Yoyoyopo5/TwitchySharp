namespace TwitchySharp.Api.Tests.E2E;

public class ClientConfiguration
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}

public static class ClientConfigurationExtensions
{
    public static Client ToClient(this ClientConfiguration config)
        => new()
        {
            Id = new(config.ClientId),
            Secret = new(config.ClientSecret)
        };
}
