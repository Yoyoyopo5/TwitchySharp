using System.Collections.Immutable;
using System.Text.Json;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_DefaultTokenResolver
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly UserId TestUserId = new("123456789");
    private static readonly UserIdentity TestUserIdentity = new(TestUserId) { ClientId = TestClientId };

    private static readonly UserAccessToken UserToken = new("user_access_token");
    private static readonly UserAccessToken OverrideToken = new("override_token");

    [Fact]
    public async Task GetToken_RequestWithOverrideToken_ReturnsOverrideFirst()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken);
        var identityResolver = new IdentityTypeTokenResolver(UserAccessTokenResolver: userResolver);
        var resolver = new DefaultTokenResolver(identityResolver);
        var request = new MockAuthorizableRequestWithOverride(TestUserIdentity, OverrideToken);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(OverrideToken.Value, hasToken.AccessTokenDetails?.Value);
        Assert.False(userResolver.WasCalled); // Should not call user resolver when override is present
    }

    [Fact]
    public async Task GetToken_UserIdentityNoOverride_UsesIdentityResolver()
    {
        // Arrange
        var userResolver = new MockUserAccessTokenResolver(UserToken);
        var identityResolver = new IdentityTypeTokenResolver(UserAccessTokenResolver: userResolver);
        var resolver = new DefaultTokenResolver(identityResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(UserToken.Value, hasToken.AccessTokenDetails?.Value);
        Assert.True(userResolver.WasCalled);
    }

    [Fact]
    public async Task GetToken_NoResolversConfigured_ReturnsUnavailableForUserIdentity()
    {
        // Arrange
        var identityResolver = new IdentityTypeTokenResolver(); // No resolvers configured
        var resolver = new DefaultTokenResolver(identityResolver);
        var request = new MockAuthorizableRequest(TestUserIdentity);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        Assert.IsType<AccessTokenDetailsResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_NoResolversButHasOverride_ReturnsOverride()
    {
        // Arrange
        var identityResolver = new IdentityTypeTokenResolver(); // No resolvers configured
        var resolver = new DefaultTokenResolver(identityResolver);
        var request = new MockAuthorizableRequestWithOverride(TestUserIdentity, OverrideToken);

        // Act
        var result = await resolver.ResolveAsync(request);

        // Assert
        var hasToken = Assert.IsAssignableFrom<IHaveAccessTokenDetails<AccessToken>>(result);
        Assert.Equal(OverrideToken.Value, hasToken.AccessTokenDetails?.Value);
    }

    #region Mock Types

    private record MockAuthorizableRequest(TwitchApiIdentity Identity) : ITwitchRequest, IAuthorizedTwitchRequest
    {
        public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet.Create(Scope.ChannelModerate);
        public AccessToken? OverrideAccessToken => null;

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private record MockAuthorizableRequestWithOverride(TwitchApiIdentity Identity, AccessToken? OverrideAccessToken)
        : ITwitchRequest, IAuthorizedTwitchRequest
    {
        public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet.Create(Scope.ChannelModerate);

        public HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions) => new();
    }

    private class MockUserAccessTokenResolver(UserAccessToken token) : IResolveUserAccessToken
    {
        public bool WasCalled { get; private set; }

        public ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(UserAccessTokenKey key, CancellationToken ct = default)
        {
            WasCalled = true;
            return ValueTask.FromResult<AccessTokenDetailsResolutionResult>(new AccessTokenDetailsResolutionResult.Valid<UserAccessToken>(token));
        }
    }

    #endregion
}
