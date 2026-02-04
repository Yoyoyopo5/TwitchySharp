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
        Assert.NotNull(result);
        Assert.Equal(UserToken.Value, result.Value);
        Assert.True(userResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_UserIdentityWithoutUserResolver_ReturnsNull()
    {
        // Arrange
        var resolver = new IdentityTokenResolver(); // No resolvers configured
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
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
        Assert.NotNull(result);
        Assert.Equal(AppToken.Value, result.Value);
        Assert.True(appResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_ClientIdentityWithoutAppResolver_ReturnsNull()
    {
        // Arrange
        var resolver = new IdentityTokenResolver(); // No resolvers configured
        var request = new MockAuthorizableRequest(TestClientIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
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
        Assert.NotNull(result);
        Assert.Equal(ExtensionToken.Value, result.Value);
        Assert.True(extensionResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_ExtensionIdentityWithoutExtensionResolver_ReturnsNull()
    {
        // Arrange
        var resolver = new IdentityTokenResolver(); // No resolvers configured
        var request = new MockAuthorizableRequest(TestExtensionIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
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
        Assert.NotNull(result);
        Assert.Equal(AppToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_DefaultIdentity_ReturnsNull()
    {
        // Arrange
        var appResolver = new MockAppAccessTokenResolver(AppToken);
        var resolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
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
        var userResolver = new MockUserAccessTokenResolver(UserToken);
        var appResolver = new MockAppAccessTokenResolver(AppToken);
        var resolver = new IdentityTokenResolver(userResolver, appResolver);
        var request = new MockNonAuthorizableRequest();

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetToken_UserResolverReturnsSuccess_ExtractsToken()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken, MockResultType.Success);
        var resolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(UserToken.Value, result.Value);
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
        Assert.NotNull(result);
        Assert.Equal(UserToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_UserResolverReturnsRequiresNewAuth_ReturnsNull()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken, MockResultType.RequiresNewAuth);
        var resolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetToken_UserResolverReturnsUnavailable_ReturnsNull()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken, MockResultType.Unavailable);
        var resolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    #region Mock Types

    private enum MockResultType { Success, Expired, RequiresNewAuth, Unavailable }

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet.Create(Scope.ChannelModerate);
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockNonAuthorizableRequest() : ITwitchRequest
    {
        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private class MockUserAccessTokenResolver : IResolveUserAccessToken
    {
        private readonly UserAccessToken _token;
        private readonly MockResultType _resultType;

        public bool WasCalled { get; private set; }

        public MockUserAccessTokenResolver(UserAccessToken token, MockResultType resultType = MockResultType.Success)
        {
            _token = token;
            _resultType = resultType;
        }

        public ValueTask<UserAccessTokenResolutionResult> GetToken(UserAccessTokenKey key, CancellationToken ct = default)
        {
            WasCalled = true;
            UserAccessTokenResolutionResult result = _resultType switch
            {
                MockResultType.Success => new UserAccessTokenResolutionResult.Success(_token),
                MockResultType.Expired => new UserAccessTokenResolutionResult.Expired(_token),
                MockResultType.RequiresNewAuth => new UserAccessTokenResolutionResult.RequiresNewAuthorization(),
                MockResultType.Unavailable => new UserAccessTokenResolutionResult.Unavailable(),
                _ => new UserAccessTokenResolutionResult.Success(_token)
            };
            return ValueTask.FromResult(result);
        }
    }

    private class MockAppAccessTokenResolver(AppAccessToken token) : IResolveAppAccessToken
    {
        public bool WasCalled { get; private set; }

        public ValueTask<AppAccessToken?> GetToken(ClientIdentity identity, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<AppAccessToken?>(token);
        }
    }

    private class MockExtensionJwtResolver(ExtensionJsonWebToken token) : IResolveExtensionJsonWebToken
    {
        public bool WasCalled { get; private set; }

        public ValueTask<ExtensionJsonWebToken?> GetToken(ExtensionIdentity identity, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<ExtensionJsonWebToken?>(token);
        }
    }

    #endregion
}
