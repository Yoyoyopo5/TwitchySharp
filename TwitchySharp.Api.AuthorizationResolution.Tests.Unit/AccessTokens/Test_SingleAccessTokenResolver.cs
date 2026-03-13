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
        var resolver = new SingleAccessTokenResolver<IAuthorizedTwitchRequest, UserAccessToken>(ConfiguredToken);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(ConfiguredToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    [Fact]
    public async Task GetToken_NonAuthorizableRequest_StillReturnsConfiguredToken()
    {
        // Arrange - Note: SingleAccessTokenResolver<TKey, TToken> works with any key type
        var resolver = new SingleAccessTokenResolver<object, UserAccessToken>(ConfiguredToken);
        var request = new object();

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(ConfiguredToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    [Fact]
    public async Task GetToken_RequestWithOverrideToken_IgnoresOverrideAndReturnsConfigured()
    {
        // Arrange
        var overrideToken = new UserAccessToken("override_token");
        var resolver = new SingleAccessTokenResolver<IAuthorizedTwitchRequest, UserAccessToken>(ConfiguredToken);
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, overrideToken);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(ConfiguredToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    [Fact]
    public async Task GetToken_MultipleCallsWithDifferentRequests_ReturnsSameToken()
    {
        // Arrange
        var resolver = new SingleAccessTokenResolver<IAuthorizedTwitchRequest, UserAccessToken>(ConfiguredToken);
        var request1 = new MockAuthorizableRequest(TwitchApiIdentity.Default);
        var request2 = new MockAuthorizableRequest(new ClientIdentity(new ClientId("other")));
        var request3 = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result1 = await resolver.ResolveAsync(request1);
        var result2 = await resolver.ResolveAsync(request2);
        var result3 = await resolver.ResolveAsync(request3);

        // Assert
        var token1 = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result1);
        var token2 = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result2);
        var token3 = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result3);
        Assert.Equal(token1.AccessTokenDetails?.Value, token2.AccessTokenDetails?.Value);
        Assert.Equal(token2.AccessTokenDetails?.Value, token3.AccessTokenDetails?.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IAuthorizedTwitchRequest
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockAuthorizableRequestWithOverride(TwitchApiIdentity Identity, AccessToken? OverrideAccessToken)
        : ITwitchRequest, IAuthorizedTwitchRequest
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    #endregion
}
