using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_DefaultRequestAuthorizer
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly ClientIdentity TestClientIdentity = new(TestClientId);
    private static readonly UserAccessToken TestAccessToken = new("test_access_token");
    private static readonly AppAccessToken TestAppToken = new("test_app_access_token");

    [Fact]
    public async Task GetAuthorization_NonAuthorizableRequest_ReturnsNull()
    {
        // Arrange
        var appResolver = new MockAppAccessTokenResolver(TestAppToken);
        var identityResolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var authorizer = new DefaultRequestAuthorizer(TestClientIdentity, identityResolver);
        var request = new MockNonAuthorizableRequest();

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAuthorization_AuthorizableRequest_ReturnsClientIdAndToken()
    {
        // Arrange
        var appResolver = new MockAppAccessTokenResolver(TestAppToken);
        var identityResolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var authorizer = new DefaultRequestAuthorizer(TestClientIdentity, identityResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestClientId.Value, result.ClientId?.Value);
        Assert.Equal(TestAppToken.Value, result.BearerToken?.Value);
    }

    [Fact]
    public async Task GetAuthorization_NoTokenResolverConfigured_ResultHasNullToken()
    {
        // Arrange
        var identityResolver = new IdentityTypeTokenResolver(); // No resolvers
        var authorizer = new DefaultRequestAuthorizer(TestClientIdentity, identityResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestClientId.Value, result.ClientId?.Value);
        Assert.Null(result.BearerToken);
    }

    [Fact]
    public async Task GetAuthorization_MultipleRequestTypes_HandledCorrectly()
    {
        // Arrange
        var appResolver = new MockAppAccessTokenResolver(TestAppToken);
        var identityResolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var authorizer = new DefaultRequestAuthorizer(TestClientIdentity, identityResolver);

        var authorizableRequest = new MockAuthorizableRequest(TwitchApiIdentity.Default);
        var nonAuthorizableRequest = new MockNonAuthorizableRequest();

        // Act
        var authorizableResult = await authorizer.GetAuthorization(authorizableRequest);
        var nonAuthorizableResult = await authorizer.GetAuthorization(nonAuthorizableRequest);

        // Assert
        Assert.NotNull(authorizableResult);
        Assert.Null(nonAuthorizableResult);
    }

    [Fact]
    public async Task GetAuthorization_RequestWithClientIdentity_UsesRequestClientIdentity()
    {
        // Arrange
        var requestClientId = new ClientId("request_client_id");
        var appResolver = new MockAppAccessTokenResolver(TestAppToken);
        var identityResolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var authorizer = new DefaultRequestAuthorizer(TestClientIdentity, identityResolver);
        var request = new MockAuthorizableRequest(new ClientIdentity(requestClientId));

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(requestClientId.Value, result.ClientId?.Value);
    }

    [Fact]
    public async Task GetAuthorization_RequestWithOverrideToken_UsesOverrideToken()
    {
        // Arrange
        var overrideToken = new UserAccessToken("override_token");
        var appResolver = new MockAppAccessTokenResolver(TestAppToken);
        var identityResolver = new IdentityTokenResolver(AppAccessTokenResolver: appResolver);
        var authorizer = new DefaultRequestAuthorizer(TestClientIdentity, identityResolver);
        var request = new MockAuthorizableRequestWithOverride(TwitchApiIdentity.Default, overrideToken);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(overrideToken.Value, result.BearerToken?.Value);
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

    private class MockAppAccessTokenResolver(AppAccessToken token) : IResolveAppAccessToken
    {
        public ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(ClientIdentity identity, CancellationToken ct = default)
            => ValueTask.FromResult<AccessTokenDetailsResolutionResult>(new AccessTokenDetailsResolutionResult.Valid<AppAccessToken>(token));
    }

    #endregion
}
