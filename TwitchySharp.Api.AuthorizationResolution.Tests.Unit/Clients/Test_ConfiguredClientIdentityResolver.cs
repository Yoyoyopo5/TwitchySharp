using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_ConfiguredClientIdentityResolver
{
    private const string ConfiguredClientIdValue = "configured_client_id";
    private static readonly ClientIdentity ConfiguredClientIdentity = new(new ClientId(ConfiguredClientIdValue));

    [Fact]
    public async Task GetClientId_RequestWithClientIdentity_ReturnsClientIdentity()
    {
        // Arrange
        var resolver = new ConfiguredClientIdentityResolver();
        var request = new MockAuthorizableRequest(ConfiguredClientIdentity);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredClientIdValue, result.ClientId.Value);
    }

    [Fact]
    public async Task GetClientId_RequestWithUserIdentity_ReturnsClientIdFromUserIdentity()
    {
        // Arrange
        var resolver = new ConfiguredClientIdentityResolver();
        var userIdentity = new UserIdentity(new UserId("123"))
        {
            ClientId = new ClientId(ConfiguredClientIdValue)
        };
        var request = new MockAuthorizableRequest(userIdentity);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredClientIdValue, result.ClientId.Value);
    }

    [Fact]
    public async Task GetClientId_RequestWithDefaultIdentity_ReturnsNull()
    {
        // Arrange
        var resolver = new ConfiguredClientIdentityResolver();
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetClientId_NonAuthorizableRequest_ReturnsNull()
    {
        // Arrange
        var resolver = new ConfiguredClientIdentityResolver();
        var request = new MockNonAuthorizableRequest();

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetClientId_RequestWithNoneIdentity_ReturnsClientIdentityWithEmptyClientId()
    {
        // Arrange
        var resolver = new ConfiguredClientIdentityResolver();
        var request = new MockAuthorizableRequest(TwitchApiIdentity.None);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("", result.ClientId.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IAuthorizedTwitchRequest
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockNonAuthorizableRequest() : ITwitchRequest
    {
        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    #endregion
}
