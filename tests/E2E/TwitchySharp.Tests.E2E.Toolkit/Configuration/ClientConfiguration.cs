using TwitchySharp.Api;

namespace TwitchySharp.Tests.E2E;

public class ClientConfiguration
{
    public required ClientId ClientId { get; set; }
    public required ClientSecret ClientSecret { get; set; }
}
