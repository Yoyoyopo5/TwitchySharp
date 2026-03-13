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
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(OverrideToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    [Fact]
    public async Task GetToken_RequestWithNullOverrideToken_ReturnsUnavailable()
    {
        // Arrange
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, null);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        Assert.IsType<AccessTokenDetailsResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_AuthorizableRequestWithoutOverride_ReturnsUnavailable()
    {
        // Arrange
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        Assert.IsType<AccessTokenDetailsResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_RequestWithAppAccessTokenOverride_ReturnsAppAccessToken()
    {
        // Arrange
        var appToken = new AppAccessToken("app_override_token");
        var resolver = new ConfiguredAccessTokenResolver();
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, appToken);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(appToken.Value, hasToken.AccessTokenDetails?.Value);
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
