using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_IdentityTokenResolver
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly ClientIdentity TestClientIdentity = new(TestClientId);
    private static readonly UserId TestUserId = new("123456789");
    private static readonly UserIdentity TestUserIdentity = new(TestUserId) { ClientId = TestClientId };
    private static readonly ExtensionIdentity TestExtensionIdentity = new(TestUserId);

    private static readonly UserAccessToken UserToken = new("user_access_token");
    private static readonly AppAccessToken AppToken = new("app_access_token");
    private static readonly ExtensionJsonWebToken ExtensionToken = new("extension_jwt");

    [Fact]
    public async Task GetToken_UserIdentityWithUserResolver_CallsUserResolver()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken);
        var resolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessToken<AccessToken>>(result);
        Assert.Equal(UserToken.Value, hasToken.AccessToken?.Value);
        Assert.True(userResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_UserIdentityWithoutUserResolver_ReturnsUnavailable()
    {
        // Arrange
        var resolver = new IdentityTokenResolver(); // No resolvers configured
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.IsType<AccessTokenResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_ClientIdentityWithAppResolver_CallsAppResolver()
    {
        // Arrange
        var appResolver = new MockAppAccessTokenResolver(AppToken);
        var resolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var request = new MockAuthorizableRequest(TestClientIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessToken<AccessToken>>(result);
        Assert.Equal(AppToken.Value, hasToken.AccessToken?.Value);
        Assert.True(appResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_ClientIdentityWithoutAppResolver_ReturnsUnavailable()
    {
        // Arrange
        var resolver = new IdentityTokenResolver(); // No resolvers configured
        var request = new MockAuthorizableRequest(TestClientIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.IsType<AccessTokenResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_ExtensionIdentityWithExtensionResolver_CallsExtensionResolver()
    {
        // Arrange
        var extensionResolver = new MockExtensionJwtResolver(ExtensionToken);
        var resolver = new IdentityTokenResolver(ExtensionJwtResolver: extensionResolver);
        var request = new MockAuthorizableRequest(TestExtensionIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessToken<AccessToken>>(result);
        Assert.Equal(ExtensionToken.Value, hasToken.AccessToken?.Value);
        Assert.True(extensionResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_ExtensionIdentityWithoutExtensionResolver_ReturnsUnavailable()
    {
        // Arrange
        var resolver = new IdentityTokenResolver(); // No resolvers configured
        var request = new MockAuthorizableRequest(TestExtensionIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.IsType<AccessTokenResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_TwitchApiIdentityWithClientId_CallsAppResolver()
    {
        // Arrange
        var appResolver = new MockAppAccessTokenResolver(AppToken);
        var resolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var identity = new TwitchApiIdentity() { ClientId = TestClientId };
        var request = new MockAuthorizableRequest(identity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessToken<AccessToken>>(result);
        Assert.Equal(AppToken.Value, hasToken.AccessToken?.Value);
    }

    [Fact]
    public async Task GetToken_DefaultIdentity_ReturnsUnavailable()
    {
        // Arrange
        var appResolver = new MockAppAccessTokenResolver(AppToken);
        var resolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.IsType<AccessTokenResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_UserResolverReturnsValid_ExtractsToken()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken, MockResultType.Valid);
        var resolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessToken<AccessToken>>(result);
        Assert.Equal(UserToken.Value, hasToken.AccessToken?.Value);
    }

    [Fact]
    public async Task GetToken_UserResolverReturnsExpired_ExtractsExpiredToken()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken, MockResultType.Expired);
        var resolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessToken<AccessToken>>(result);
        Assert.Equal(UserToken.Value, hasToken.AccessToken?.Value);
    }

    [Fact]
    public async Task GetToken_UserResolverReturnsUnavailable_ReturnsUnavailable()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken, MockResultType.Unavailable);
        var resolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.IsType<AccessTokenResolutionResult.Unavailable>(result);
    }

    #region Mock Types

    private enum MockResultType { Valid, Expired, Unavailable }

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet.Create(Scope.ChannelModerate);
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private class MockUserAccessTokenResolver : IResolveUserAccessToken
    {
        private readonly UserAccessToken _token;
        private readonly MockResultType _resultType;

        public bool WasCalled { get; private set; }

        public MockUserAccessTokenResolver(UserAccessToken token, MockResultType resultType = MockResultType.Valid)
        {
            _token = token;
            _resultType = resultType;
        }

        public ValueTask<AccessTokenResolutionResult> GetToken(UserAccessTokenKey key, CancellationToken ct = default)
        {
            WasCalled = true;
            AccessTokenResolutionResult result = _resultType switch
            {
                MockResultType.Valid => new AccessTokenResolutionResult.Valid<UserAccessToken>(_token),
                MockResultType.Expired => new AccessTokenResolutionResult.Expired<UserAccessToken>(_token),
                MockResultType.Unavailable => AccessTokenResolutionResult.Unavailable.Instance,
                _ => new AccessTokenResolutionResult.Valid<UserAccessToken>(_token)
            };
            return ValueTask.FromResult(result);
        }
    }

    private class MockAppAccessTokenResolver(AppAccessToken token) : IResolveAppAccessToken
    {
        public bool WasCalled { get; private set; }

        public ValueTask<AccessTokenResolutionResult> GetToken(ClientIdentity identity, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<AccessTokenResolutionResult>(new AccessTokenResolutionResult.Valid<AppAccessToken>(token));
        }
    }

    private class MockExtensionJwtResolver(ExtensionJsonWebToken token) : IResolveExtensionJsonWebToken
    {
        public bool WasCalled { get; private set; }

        public ValueTask<AccessTokenResolutionResult> GetToken(ExtensionIdentity identity, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<AccessTokenResolutionResult>(new AccessTokenResolutionResult.Valid<ExtensionJsonWebToken>(token));
        }
    }

    #endregion
}
