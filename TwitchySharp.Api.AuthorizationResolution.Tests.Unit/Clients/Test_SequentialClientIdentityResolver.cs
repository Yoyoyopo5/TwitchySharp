using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_SequentialClientIdentityResolver
{
    private const string FirstClientIdValue = "first_client_id";
    private const string SecondClientIdValue = "second_client_id";
    private static readonly ClientIdentity FirstClientIdentity = new(new ClientId(FirstClientIdValue));
    private static readonly ClientIdentity SecondClientIdentity = new(new ClientId(SecondClientIdValue));

    [Fact]
    public async Task GetClientId_FirstResolverReturnsValue_ReturnsFirstValue()
    {
        // Arrange
        var firstResolver = new SingleClientIdentityResolver(FirstClientIdentity);
        var secondResolver = new SingleClientIdentityResolver(SecondClientIdentity);
        var resolver = new SequentialClientIdentityResolver([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(FirstClientIdValue, result.ClientId.Value);
    }

    [Fact]
    public async Task GetClientId_FirstResolverReturnsNull_ReturnsSecondValue()
    {
        // Arrange
        var firstResolver = new MockNullClientIdentityResolver();
        var secondResolver = new SingleClientIdentityResolver(SecondClientIdentity);
        var resolver = new SequentialClientIdentityResolver([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(SecondClientIdValue, result.ClientId.Value);
    }

    [Fact]
    public async Task GetClientId_AllResolversReturnNull_ReturnsNull()
    {
        // Arrange
        var firstResolver = new MockNullClientIdentityResolver();
        var secondResolver = new MockNullClientIdentityResolver();
        var resolver = new SequentialClientIdentityResolver([firstResolver, secondResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetClientId_EmptyResolverChain_ReturnsNull()
    {
        // Arrange
        var resolver = new SequentialClientIdentityResolver([]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetClientId_StopsAtFirstNonNullResult_DoesNotCallSubsequentResolvers()
    {
        // Arrange
        var firstResolver = new SingleClientIdentityResolver(FirstClientIdentity);
        var trackingResolver = new MockTrackingClientIdentityResolver(SecondClientIdentity);
        var resolver = new SequentialClientIdentityResolver([firstResolver, trackingResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        await resolver.GetClientId(request);

        // Assert
        Assert.False(trackingResolver.WasCalled);
    }

    [Fact]
    public async Task GetClientId_SkipsNullResolvers_ContinuesToNext()
    {
        // Arrange
        var firstResolver = new MockNullClientIdentityResolver();
        var trackingResolver = new MockTrackingClientIdentityResolver(SecondClientIdentity);
        var resolver = new SequentialClientIdentityResolver([firstResolver, trackingResolver]);
        var request = new MockAuthorizableRequest(TwitchApiIdentity.Default);

        // Act
        var result = await resolver.GetClientId(request);

        // Assert
        Assert.True(trackingResolver.WasCalled);
        Assert.NotNull(result);
        Assert.Equal(SecondClientIdValue, result.ClientId.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IAuthorizedTwitchRequest
    {
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private class MockNullClientIdentityResolver : IResolveClientIdentity
    {
        public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
            => ValueTask.FromResult<ClientIdentity?>(null);
    }

    private class MockTrackingClientIdentityResolver(ClientIdentity identity) : IResolveClientIdentity
    {
        public bool WasCalled { get; private set; }

        public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<ClientIdentity?>(identity);
        }
    }

    #endregion
}
