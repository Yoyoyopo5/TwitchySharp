using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_ConfiguredAccessTokenResolver
{
    private static readonly UserAccessToken OverrideToken = new("override_token");

    [Fact]
    public async Task GetToken_RequestWithOverrideToken_ReturnsOverrideToken()
    {
        // Arrange
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, OverrideToken);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(OverrideToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_RequestWithNullOverrideToken_ReturnsNull()
    {
        // Arrange
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, null);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetToken_AuthorizableRequestWithoutOverride_ReturnsNull()
    {
        // Arrange
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetToken_NonAuthorizableRequest_ReturnsNull()
    {
        // Arrange
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockNonAuthorizableRequest();

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetToken_RequestWithAppAccessTokenOverride_ReturnsAppAccessToken()
    {
        // Arrange
        var appToken = new AppAccessToken("app_override_token");
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, appToken);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(appToken.Value, result.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockAuthorizableRequestWithOverride(TwitchApiIdentity Identity, AccessToken? OverrideAccessToken)
        : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockNonAuthorizableRequest() : ITwitchRequest
    {
        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    #endregion
}
