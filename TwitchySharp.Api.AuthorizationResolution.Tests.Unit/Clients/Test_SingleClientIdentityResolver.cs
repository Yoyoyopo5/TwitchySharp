using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_SingleClientIdentityResolver
{
    private const string ConfiguredClientIdValue = "configured_client_id";
    private static readonly ClientIdentity ConfiguredClientIdentity = new(new ClientId(ConfiguredClientIdValue));

    [Fact]
    public async Task GetClientId_AnyRequest_ReturnsConfiguredIdentity()
    {
        // Arrange
        var resolver = new SingleClientIdentityResolver(ConfiguredClientIdentity);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredClientIdValue, result.ClientId.Value);
    }

    [Fact]
    public async Task GetClientId_NonAuthorizableRequest_StillReturnsConfiguredIdentity()
    {
        // Arrange
        var resolver = new SingleClientIdentityResolver(ConfiguredClientIdentity);
        var request = new MockNonAuthorizableRequest();

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredClientIdValue, result.ClientId.Value);
    }

    [Fact]
    public async Task GetClientId_RequestWithDifferentIdentity_IgnoresRequestIdentityAndReturnsConfigured()
    {
        // Arrange
        var differentClientId = new ClientId("different_client_id");
        var resolver = new SingleClientIdentityResolver(ConfiguredClientIdentity);
        var request = new MockAuthorizableRequest(new ClientIdentity(differentClientId));

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredClientIdValue, result.ClientId.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
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
