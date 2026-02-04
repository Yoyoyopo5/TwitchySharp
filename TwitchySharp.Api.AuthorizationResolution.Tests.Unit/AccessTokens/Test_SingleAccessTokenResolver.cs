using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_SingleAccessTokenResolver
{
    private static readonly UserAccessToken ConfiguredToken = new("configured_access_token");

    [Fact]
    public async Task GetToken_AnyRequest_ReturnsConfiguredToken()
    {
        // Arrange
        var resolver = new SingleAccessTokenResolver(ConfiguredToken);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_NonAuthorizableRequest_StillReturnsConfiguredToken()
    {
        // Arrange
        var resolver = new SingleAccessTokenResolver(ConfiguredToken);
        var request = new MockNonAuthorizableRequest();

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_RequestWithOverrideToken_IgnoresOverrideAndReturnsConfigured()
    {
        // Arrange
        var overrideToken = new UserAccessToken("override_token");
        var resolver = new SingleAccessTokenResolver(ConfiguredToken);
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, overrideToken);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ConfiguredToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_MultipleCallsWithDifferentRequests_ReturnsSameToken()
    {
        // Arrange
        var resolver = new SingleAccessTokenResolver(ConfiguredToken);
        var request1 = new MockAuthorizableRequest(TwitchApiIdentity.Default);
        var request2 = new MockAuthorizableRequest(new ClientIdentity(new ClientId("other")));
        var request3 = new MockNonAuthorizableRequest();

        // Act
        var result1 = await resolver.GetToken(request1);
        var result2 = await resolver.GetToken(request2);
        var result3 = await resolver.GetToken(request3);

        // Assert
        Assert.Equal(result1, result2);
        Assert.Equal(result2, result3);
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
