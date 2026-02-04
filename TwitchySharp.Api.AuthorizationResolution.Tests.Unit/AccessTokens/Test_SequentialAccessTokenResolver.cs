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
        var firstResolver = new SingleAccessTokenResolver(FirstToken);
        var secondResolver = new SingleAccessTokenResolver(SecondToken);
        var resolver = new SequentialAccessTokenResolver([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(FirstToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_FirstResolverReturnsNull_ReturnsSecondValue()
    {
        // Arrange
        var firstResolver = new MockNullTokenResolver();
        var secondResolver = new SingleAccessTokenResolver(SecondToken);
        var resolver = new SequentialAccessTokenResolver([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(SecondToken.Value, result.Value);
    }

    [Fact]
    public async Task GetToken_AllResolversReturnNull_ReturnsNull()
    {
        // Arrange
        var firstResolver = new MockNullTokenResolver();
        var secondResolver = new MockNullTokenResolver();
        var resolver = new SequentialAccessTokenResolver([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetToken_EmptyResolverChain_ReturnsNull()
    {
        // Arrange
        var resolver = new SequentialAccessTokenResolver([]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetToken_StopsAtFirstNonNullResult_DoesNotCallSubsequentResolvers()
    {
        // Arrange
        var firstResolver = new SingleAccessTokenResolver(FirstToken);
        var trackingResolver = new MockTrackingTokenResolver(SecondToken);
        var resolver = new SequentialAccessTokenResolver([firstResolver, trackingResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        await resolver.GetToken(request);

        // Assert
        Assert.False(trackingResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_SkipsNullResolvers_ContinuesToNext()
    {
        // Arrange
        var firstResolver = new MockNullTokenResolver();
        var trackingResolver = new MockTrackingTokenResolver(SecondToken);
        var resolver = new SequentialAccessTokenResolver([firstResolver, trackingResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetToken(request);

        // Assert
        Assert.True(trackingResolver.WasCalled);
        Assert.NotNull(result);
        Assert.Equal(SecondToken.Value, result.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IRequireAuthorization
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private class MockNullTokenResolver : ITokenResolver
    {
        public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
            => ValueTask.FromResult<AccessToken?>(null);
    }

    private class MockTrackingTokenResolver(AccessToken token) : ITokenResolver
    {
        public bool WasCalled { get; private set; }

        public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<AccessToken?>(token);
        }
    }

    #endregion
}
