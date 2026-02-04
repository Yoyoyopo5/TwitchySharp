using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_SingleClientSecretResolver
{
    private static readonly ClientId ConfiguredClientId = new("configured_client_id");
    private static readonly ClientSecret ConfiguredClientSecret = new("configured_client_secret");

    [Fact]
    public async Task GetClientSecret_MatchingClientId_ReturnsConfiguredSecret()
    {
        // Arrange
        var resolver = new SingleClientSecretResolver(ConfiguredClientId, ConfiguredClientSecret);

        // Act
        var result = await resolver.GetClientSecret(ConfiguredClientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredClientSecret.Value, result.Value.Value);
    }

    [Fact]
    public async Task GetClientSecret_DifferentClientId_ReturnsNull()
    {
        // Arrange
        var resolver = new SingleClientSecretResolver(ConfiguredClientId, ConfiguredClientSecret);
        var differentClientId = new ClientId("different_client_id");

        // Act
        var result = await resolver.GetClientSecret(differentClientId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetClientSecret_EmptyClientId_ReturnsNullIfNotConfiguredForEmpty()
    {
        // Arrange
        var resolver = new SingleClientSecretResolver(ConfiguredClientId, ConfiguredClientSecret);
        var emptyClientId = new ClientId("");

        // Act
        var result = await resolver.GetClientSecret(emptyClientId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetClientSecret_ClientIdWithSameValue_MatchesCorrectly()
    {
        // Arrange
        var resolver = new SingleClientSecretResolver(ConfiguredClientId, ConfiguredClientSecret);
        var sameValueClientId = new ClientId("configured_client_id"); // Same value, different instance

        // Act
        var result = await resolver.GetClientSecret(sameValueClientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredClientSecret.Value, result.Value.Value);
    }
}
