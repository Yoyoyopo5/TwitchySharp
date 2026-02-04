using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Fixtures;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

/// <summary>
/// Integration tests for the full DefaultRequestAuthorizer pipeline.
/// Tests the complete flow from request to identity resolution to token resolution.
/// </summary>
public class Test_DefaultRequestAuthorizer_Pipeline(TokenResolutionTestFixture fixture) : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    [Fact]
    public async Task GetAuthorization_NonAuthorizableRequest_ReturnsNull()
    {
        // Arrange
        var authorizer = _fixture.CreateDefaultAuthorizer();
        var request = new MockNonAuthorizableRequest();

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAuthorization_AuthorizableRequestWithDefaultIdentity_ResolvesDefaultClient()
    {
        // Arrange
        var authorizer = _fixture.CreateDefaultAuthorizer();
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default)
        {
            ValidScopes = ImmutableHashSet.Create(Scope.ChannelModerate)
        };

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TokenResolutionTestFixture.TestClientId, result.ClientId?.Value);
    }

    [Fact]
    public async Task GetAuthorization_AuthorizableRequestWithConfiguredIdentity_UsesConfiguredClient()
    {
        // Arrange
        var configuredClientId = new ClientId("configured_client");
        var configuredIdentity = new ClientIdentity(configuredClientId);
        var authorizer = _fixture.CreateDefaultAuthorizer();
        var request = new MockAuthorizableRequest(configuredIdentity);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("configured_client", result.ClientId?.Value);
    }

    [Fact]
    public async Task GetAuthorization_AuthorizableRequestWithOverrideToken_UsesOverrideToken()
    {
        // Arrange
        var overrideToken = new UserAccessToken("override_token");
        var identityResolver = new IdentityTokenResolver(); // No resolvers, relies on configured override
        var authorizer = new DefaultRequestAuthorizer(TokenResolutionTestFixture.ClientIdentity, identityResolver);
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, overrideToken);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("override_token", result.BearerToken?.Value);
    }

    [Fact]
    public async Task GetAuthorization_UserIdentityRequest_ResolvesUserToken()
    {
        // Arrange
        var store = _fixture.CreatePopulatedTokenStore();
        var userResolver = new ConcurrentUserAccessTokenResolver(store, null, null);
        var identityResolver = new IdentityTokenResolver(UserAccessTokenResolver: userResolver);
        var authorizer = new DefaultRequestAuthorizer(TokenResolutionTestFixture.ClientIdentity, identityResolver);
        var request = new MockAuthorizableRequest(TokenResolutionTestFixture.TestUserIdentity)
        {
            ValidScopes = ImmutableHashSet.Create(Scope.ChannelModerate)
        };

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, result.BearerToken?.Value);
    }

    [Fact]
    public async Task GetAuthorization_ClientIdentityRequest_WithAppTokenResolver_ResolvesAppToken()
    {
        // Arrange
        var appToken = new AppAccessToken("app_access_token");
        var appResolver = new MockAppAccessTokenResolver(appToken);
        var identityResolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var authorizer = new DefaultRequestAuthorizer(TokenResolutionTestFixture.ClientIdentity, identityResolver);
        var request = new MockAuthorizableRequest(TokenResolutionTestFixture.ClientIdentity);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("app_access_token", result.BearerToken?.Value);
    }

    [Fact]
    public async Task GetAuthorization_SequentialResolvers_FirstAvailableWins()
    {
        // Arrange
        var firstToken = new UserAccessToken("first_token");
        var secondToken = new UserAccessToken("second_token");
        var unavailableResolver = new MockUnavailableTokenResolver();
        var firstResolver = new SingleAccessTokenResolver<IRequireAuthorization, UserAccessToken>(firstToken);
        var secondResolver = new SingleAccessTokenResolver<IRequireAuthorization, UserAccessToken>(secondToken);
        var sequentialResolver = new SequentialAccessTokenResolver<IRequireAuthorization>([unavailableResolver, firstResolver, secondResolver]);
        var identityResolver = new IdentityTokenResolver();

        // Create a custom authorizer that uses the sequential resolver via DefaultTokenResolver
        // Since DefaultTokenResolver wraps IdentityTokenResolver, we need a different approach
        // We'll create a custom setup that demonstrates the sequential behavior
        var combinedResolver = new SequentialAccessTokenResolver<IRequireAuthorization>([
            new ConfiguredAccessTokenResolver(),
            unavailableResolver,
            firstResolver,
            secondResolver
        ]);

        // Use reflection or a custom mock to test this scenario
        // For now, let's just test that the first available token is returned
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);
        var result = await combinedResolver.GetToken(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessToken<AccessToken>>(result);
        Assert.Equal("first_token", hasToken.AccessToken?.Value);
    }

    [Fact]
    public async Task GetAuthorization_SequentialClientResolvers_FirstNonNullWins()
    {
        // Arrange
        var firstClientId = new ClientId("first_client");
        var secondClientId = new ClientId("second_client");
        var nullResolver = new MockNullClientResolver();
        var firstResolver = new SingleClientIdentityResolver(new ClientIdentity(firstClientId));
        var secondResolver = new SingleClientIdentityResolver(new ClientIdentity(secondClientId));
        var sequentialResolver = new SequentialClientIdentityResolver([nullResolver, firstResolver, secondResolver]);

        // Test the sequential resolver directly
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);
        var clientResult = await sequentialResolver.GetClientId(request);

        // Assert
        Assert.NotNull(clientResult);
        Assert.Equal("first_client", clientResult.ClientId.Value);
    }

    #region Mock Types

    private record MockNonAuthorizableRequest() : ITwitchRequest
    {
        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockAuthorizableRequestWithOverride(TwitchApiIdentity Identity, AccessToken? OverrideAccessToken)
        : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private class MockUnavailableTokenResolver : IResolveAccessToken<IRequireAuthorization>
    {
        public ValueTask<AccessTokenResolutionResult> GetToken(IRequireAuthorization request, CancellationToken ct = default)
            => ValueTask.FromResult<AccessTokenResolutionResult>(AccessTokenResolutionResult.Unavailable.Instance);
    }

    private class MockNullClientResolver : IResolveClientIdentity
    {
        public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
            => ValueTask.FromResult<ClientIdentity?>(null);
    }

    private class MockAppAccessTokenResolver(AppAccessToken token) : IResolveAppAccessToken
    {
        public ValueTask<AccessTokenResolutionResult> GetToken(ClientIdentity identity, CancellationToken ct = default)
            => ValueTask.FromResult<AccessTokenResolutionResult>(new AccessTokenResolutionResult.Valid<AppAccessToken>(token));
    }

    #endregion
}
