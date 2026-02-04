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

    [Fact]
    public async Task GetAuthorization_NonAuthorizableRequest_ReturnsNull()
    {
        // Arrange
        var clientResolver = new SingleClientIdentityResolver(TestClientIdentity);
        var tokenResolver = new SingleAccessTokenResolver(TestAccessToken);
        var authorizer = new DefaultRequestAuthorizer(clientResolver, tokenResolver);
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
        var clientResolver = new SingleClientIdentityResolver(TestClientIdentity);
        var tokenResolver = new SingleAccessTokenResolver(TestAccessToken);
        var authorizer = new DefaultRequestAuthorizer(clientResolver, tokenResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestClientId.Value, result.ClientId?.Value);
        Assert.Equal(TestAccessToken.Value, result.BearerToken?.Value);
    }

    [Fact]
    public async Task GetAuthorization_ClientResolverReturnsNull_ResultHasNullClientId()
    {
        // Arrange
        var clientResolver = new MockNullClientIdentityResolver();
        var tokenResolver = new SingleAccessTokenResolver(TestAccessToken);
        var authorizer = new DefaultRequestAuthorizer(clientResolver, tokenResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.ClientId);
        Assert.Equal(TestAccessToken.Value, result.BearerToken?.Value);
    }

    [Fact]
    public async Task GetAuthorization_TokenResolverReturnsNull_ResultHasNullToken()
    {
        // Arrange
        var clientResolver = new SingleClientIdentityResolver(TestClientIdentity);
        var tokenResolver = new MockNullTokenResolver();
        var authorizer = new DefaultRequestAuthorizer(clientResolver, tokenResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestClientId.Value, result.ClientId?.Value);
        Assert.Null(result.BearerToken);
    }

    [Fact]
    public async Task GetAuthorization_BothResolversReturnNull_ResultHasBothNull()
    {
        // Arrange
        var clientResolver = new MockNullClientIdentityResolver();
        var tokenResolver = new MockNullTokenResolver();
        var authorizer = new DefaultRequestAuthorizer(clientResolver, tokenResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await authorizer.GetAuthorization(request);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.ClientId);
        Assert.Null(result.BearerToken);
    }

    [Fact]
    public async Task GetAuthorization_CancellationTokenPassed_PropagatedToResolvers()
    {
        // Arrange
        var clientResolver = new MockTrackingClientIdentityResolver(TestClientIdentity);
        var tokenResolver = new MockTrackingTokenResolver(TestAccessToken);
        var authorizer = new DefaultRequestAuthorizer(clientResolver, tokenResolver);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);
        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        // Act
        await authorizer.GetAuthorization(request, cancellationToken);

        // Assert
        Assert.Equal(cancellationToken, clientResolver.ReceivedCancellationToken);
        Assert.Equal(cancellationToken, tokenResolver.ReceivedCancellationToken);
    }

    [Fact]
    public async Task GetAuthorization_MultipleRequestTypes_HandledCorrectly()
    {
        // Arrange
        var clientResolver = new SingleClientIdentityResolver(TestClientIdentity);
        var tokenResolver = new SingleAccessTokenResolver(TestAccessToken);
        var authorizer = new DefaultRequestAuthorizer(clientResolver, tokenResolver);

        var authorizableRequest = new MockAuthorizableRequest(TwitchApiIdentity.Default);
        var nonAuthorizableRequest = new MockNonAuthorizableRequest();

        // Act
        var authorizableResult = await authorizer.GetAuthorization(authorizableRequest);
        var nonAuthorizableResult = await authorizer.GetAuthorization(nonAuthorizableRequest);

        // Assert
        Assert.NotNull(authorizableResult);
        Assert.Null(nonAuthorizableResult);
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

    private class MockNullClientIdentityResolver : IResolveClientIdentity
    {
        public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
            => ValueTask.FromResult<ClientIdentity?>(null);
    }

    private class MockNullTokenResolver : ITokenResolver
    {
        public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
            => ValueTask.FromResult<AccessToken?>(null);
    }

    private class MockTrackingClientIdentityResolver(ClientIdentity identity) : IResolveClientIdentity
    {
        public CancellationToken ReceivedCancellationToken { get; private set; }

        public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
        {
            ReceivedCancellationToken = ct;
            return ValueTask.FromResult<ClientIdentity?>(identity);
        }
    }

    private class MockTrackingTokenResolver(AccessToken token) : ITokenResolver
    {
        public CancellationToken ReceivedCancellationToken { get; private set; }

        public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
        {
            ReceivedCancellationToken = ct;
            return ValueTask.FromResult<AccessToken?>(token);
        }
    }

    #endregion
}
