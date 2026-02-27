using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_SequentialAccessTokenResolver
{
    private static readonly UserAccessToken FirstToken = new("first_token");
    private static readonly UserAccessToken SecondToken = new("second_token");

    [Fact]
    public async Task GetToken_FirstResolverReturnsValue_ReturnsFirstValue()
    {
        // Arrange
        var firstResolver = new SingleAccessTokenResolver<IRequireAuthorization, UserAccessToken>(FirstToken);
        var secondResolver = new SingleAccessTokenResolver<IRequireAuthorization, UserAccessToken>(SecondToken);
        var resolver = new SequentialResolver<IRequireAuthorization>([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(FirstToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    [Fact]
    public async Task GetToken_FirstResolverReturnsUnavailable_ReturnsSecondValue()
    {
        // Arrange
        var firstResolver = new MockUnavailableTokenResolver();
        var secondResolver = new SingleAccessTokenResolver<IRequireAuthorization, UserAccessToken>(SecondToken);
        var resolver = new SequentialResolver<IRequireAuthorization>([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(SecondToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    [Fact]
    public async Task GetToken_AllResolversReturnUnavailable_ReturnsUnavailable()
    {
        // Arrange
        var firstResolver = new MockUnavailableTokenResolver();
        var secondResolver = new MockUnavailableTokenResolver();
        var resolver = new SequentialResolver<IRequireAuthorization>([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        Assert.IsType<AccessTokenDetailsResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_EmptyResolverChain_ReturnsUnavailable()
    {
        // Arrange
        var resolver = new SequentialResolver<IRequireAuthorization>([]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        Assert.IsType<AccessTokenDetailsResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_StopsAtFirstAvailableResult_DoesNotCallSubsequentResolvers()
    {
        // Arrange
        var firstResolver = new SingleAccessTokenResolver<IRequireAuthorization, UserAccessToken>(FirstToken);
        var trackingResolver = new MockTrackingTokenResolver(SecondToken);
        var resolver = new SequentialResolver<IRequireAuthorization>([firstResolver, trackingResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        await resolver.ResolveAsync(request);

        // Assert
        Assert.False(trackingResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_SkipsUnavailableResolvers_ContinuesToNext()
    {
        // Arrange
        var firstResolver = new MockUnavailableTokenResolver();
        var trackingResolver = new MockTrackingTokenResolver(SecondToken);
        var resolver = new SequentialResolver<IRequireAuthorization>([firstResolver, trackingResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        Assert.True(trackingResolver.WasCalled);
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(SecondToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private class MockUnavailableTokenResolver : IResolveAccessToken<IRequireAuthorization>
    {
        public ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(IRequireAuthorization request, CancellationToken ct = default)
            => ValueTask.FromResult<AccessTokenDetailsResolutionResult>(AccessTokenDetailsResolutionResult.Unavailable.Instance);
    }

    private class MockTrackingTokenResolver(AccessToken token) : IResolveAccessToken<IRequireAuthorization>
    {
        public bool WasCalled { get; private set; }

        public ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(IRequireAuthorization request, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<AccessTokenDetailsResolutionResult>(new AccessTokenDetailsResolutionResult.Available<AccessToken>(token));
        }
    }

    #endregion
}
