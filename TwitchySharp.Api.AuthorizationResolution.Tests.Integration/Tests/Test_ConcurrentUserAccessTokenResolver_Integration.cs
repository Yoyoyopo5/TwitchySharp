using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Fixtures;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Tests;

/// <summary>
/// Integration tests for ConcurrentUserAccessTokenResolver token lifecycle:
/// store -> validate -> refresh -> store
/// </summary>
public class Test_ConcurrentUserAccessTokenResolver_Integration(TokenResolutionTestFixture fixture) : IClassFixture<TokenResolutionTestFixture>
{
    private readonly TokenResolutionTestFixture _fixture = fixture;

    [Fact]
    public async Task GetToken_ValidTokenInStore_ReturnsValidImmediately()
    {
        // Arrange
        var store = _fixture.CreatePopulatedTokenStore();
        var resolver = new ConcurrentUserAccessTokenResolver(store, null, null);
        var key = _fixture.CreateTestTokenKey();

        // Act
        var result = await resolver.GetToken(key);

        // Assert
        var valid = Assert.IsType<AccessTokenResolutionResult.Valid<UserAccessToken>>(result);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, valid.AccessToken.Value);
    }

    [Fact]
    public async Task GetToken_NoTokenInStore_ReturnsUnavailable()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var resolver = new ConcurrentUserAccessTokenResolver(store, null, null);
        var key = _fixture.CreateTestTokenKey();

        // Act
        var result = await resolver.GetToken(key);

        // Assert
        Assert.IsType<AccessTokenResolutionResult.Unavailable>(result);
    }

    [Fact]
    public async Task GetToken_ExpiredTokenWithRefresh_RefreshesAndReturnsValid()
    {
        // Arrange
        var expiredDetails = _fixture.CreateTestTokenDetails(
            expiresAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            refreshToken: TokenResolutionTestFixture.RefreshToken
        );
        var key = _fixture.CreateTestTokenKey();
        var store = _fixture.CreatePopulatedTokenStore(expiredDetails, key);
        var refresher = new MockTokenRefresher(TokenResolutionTestFixture.TestNewAccessToken);
        var resolver = new ConcurrentUserAccessTokenResolver(store, refresher, null);

        // Act
        var result = await resolver.GetToken(key);

        // Assert
        var valid = Assert.IsType<AccessTokenResolutionResult.Valid<UserAccessToken>>(result);
        Assert.Equal(TokenResolutionTestFixture.TestNewAccessToken, valid.AccessToken.Value);

        // Verify token was saved to store
        var storedToken = await store.GetTokenDetails(key);
        Assert.NotNull(storedToken);
        Assert.Equal(TokenResolutionTestFixture.TestNewAccessToken, storedToken.AccessToken.Value);
    }

    [Fact]
    public async Task GetToken_ExpiredTokenWithoutRefresher_ReturnsExpired()
    {
        // Arrange
        var expiredDetails = _fixture.CreateTestTokenDetails(
            expiresAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            refreshToken: TokenResolutionTestFixture.RefreshToken
        );
        var key = _fixture.CreateTestTokenKey();
        var store = _fixture.CreatePopulatedTokenStore(expiredDetails, key);
        var resolver = new ConcurrentUserAccessTokenResolver(store, null, null); // No refresher

        // Act
        var result = await resolver.GetToken(key);

        // Assert
        var expired = Assert.IsType<AccessTokenResolutionResult.Expired<UserAccessToken>>(result);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, expired.AccessToken.Value);
    }

    [Fact]
    public async Task GetToken_ExpiredTokenWithoutRefreshToken_ReturnsExpired()
    {
        // Arrange - Create details directly without refresh token
        var expiredDetails = new UserAccessTokenDetails
        {
            Identity = TokenResolutionTestFixture.TestUserIdentity,
            AccessToken = TokenResolutionTestFixture.AccessToken,
            RefreshToken = null, // No refresh token
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(-10),
            Scopes = ImmutableHashSet.Create(Scope.ChannelModerate)
        };
        var key = _fixture.CreateTestTokenKey();
        var store = new InMemoryUserAccessTokenStore();
        await store.SaveTokenDetails(key, expiredDetails);
        var refresher = new MockTokenRefresher(TokenResolutionTestFixture.TestNewAccessToken);
        var resolver = new ConcurrentUserAccessTokenResolver(store, refresher, null);

        // Act
        var result = await resolver.GetToken(key);

        // Assert
        var expired = Assert.IsType<AccessTokenResolutionResult.Expired<UserAccessToken>>(result);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, expired.AccessToken.Value);
    }

    [Fact]
    public async Task GetToken_ConcurrentRequestsForSameUser_OnlyRefreshesOnce()
    {
        // Arrange
        var expiredDetails = _fixture.CreateTestTokenDetails(
            expiresAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            refreshToken: TokenResolutionTestFixture.RefreshToken
        );
        var key = _fixture.CreateTestTokenKey();
        var store = _fixture.CreatePopulatedTokenStore(expiredDetails, key);
        var refresher = new MockDelayedTokenRefresher(TokenResolutionTestFixture.TestNewAccessToken, TimeSpan.FromMilliseconds(100));
        var resolver = new ConcurrentUserAccessTokenResolver(store, refresher, null);

        // Act - Fire multiple concurrent requests
        var tasks = Enumerable.Range(0, 5).Select(_ => resolver.GetToken(key).AsTask()).ToArray();
        var results = await Task.WhenAll(tasks);

        // Assert - All should succeed
        Assert.All(results, result =>
        {
            var valid = Assert.IsType<AccessTokenResolutionResult.Valid<UserAccessToken>>(result);
            Assert.Equal(TokenResolutionTestFixture.TestNewAccessToken, valid.AccessToken.Value);
        });

        // Assert - Refresh was only called once
        Assert.Equal(1, refresher.RefreshCount);
    }

    [Fact]
    public async Task GetToken_RefreshThrowsTwitchApiException_ReturnsExpired()
    {
        // Arrange
        var expiredDetails = _fixture.CreateTestTokenDetails(
            expiresAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            refreshToken: TokenResolutionTestFixture.RefreshToken
        );
        var key = _fixture.CreateTestTokenKey();
        var store = _fixture.CreatePopulatedTokenStore(expiredDetails, key);
        var refresher = new MockFailingTokenRefresher();
        var resolver = new ConcurrentUserAccessTokenResolver(store, refresher, null);

        // Act
        var result = await resolver.GetToken(key);

        // Assert - Returns expired token instead of throwing
        var expired = Assert.IsType<AccessTokenResolutionResult.Expired<UserAccessToken>>(result);
        Assert.Equal(TokenResolutionTestFixture.TestAccessToken, expired.AccessToken.Value);
    }

    [Fact]
    public async Task GetToken_TokenNearExpiration_RefreshesProactively()
    {
        // Arrange - Token expires in 2 minutes, buffer is 5 minutes
        var nearExpiryDetails = _fixture.CreateTestTokenDetails(
            expiresAt: DateTimeOffset.UtcNow.AddMinutes(2),
            refreshToken: TokenResolutionTestFixture.RefreshToken
        );
        var key = _fixture.CreateTestTokenKey();
        var store = _fixture.CreatePopulatedTokenStore(nearExpiryDetails, key);
        var refresher = new MockTokenRefresher(TokenResolutionTestFixture.TestNewAccessToken);
        var resolver = new ConcurrentUserAccessTokenResolver(store, refresher, null)
        {
            ExpirationBuffer = TimeSpan.FromMinutes(5)
        };

        // Act
        var result = await resolver.GetToken(key);

        // Assert - Token should be refreshed because it's within the buffer
        var valid = Assert.IsType<AccessTokenResolutionResult.Valid<UserAccessToken>>(result);
        Assert.Equal(TokenResolutionTestFixture.TestNewAccessToken, valid.AccessToken.Value);
    }

    [Fact]
    public async Task GetToken_DifferentUsers_ProcessedIndependently()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var user1 = new UserIdentity(new UserId("user1")) { ClientId = TokenResolutionTestFixture.ClientId };
        var user2 = new UserIdentity(new UserId("user2")) { ClientId = TokenResolutionTestFixture.ClientId };

        var details1 = new UserAccessTokenDetails
        {
            Identity = user1,
            AccessToken = new UserAccessToken("token1"),
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(4),
            Scopes = ImmutableHashSet.Create(Scope.ChannelModerate)
        };
        var details2 = new UserAccessTokenDetails
        {
            Identity = user2,
            AccessToken = new UserAccessToken("token2"),
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(4),
            Scopes = ImmutableHashSet.Create(Scope.ChannelModerate)
        };

        var key1 = new UserAccessTokenKey { User = user1, ValidScopes = ImmutableHashSet.Create(Scope.ChannelModerate) };
        var key2 = new UserAccessTokenKey { User = user2, ValidScopes = ImmutableHashSet.Create(Scope.ChannelModerate) };

        await store.SaveTokenDetails(key1, details1);
        await store.SaveTokenDetails(key2, details2);

        var resolver = new ConcurrentUserAccessTokenResolver(store, null, null);

        // Act
        var result1 = await resolver.GetToken(key1);
        var result2 = await resolver.GetToken(key2);

        // Assert
        var valid1 = Assert.IsType<AccessTokenResolutionResult.Valid<UserAccessToken>>(result1);
        var valid2 = Assert.IsType<AccessTokenResolutionResult.Valid<UserAccessToken>>(result2);
        Assert.Equal("token1", valid1.AccessToken.Value);
        Assert.Equal("token2", valid2.AccessToken.Value);
    }

    #region Mock Types

    private class MockTokenRefresher(string newAccessToken) : IRefreshUserAccessToken
    {
        public int RefreshCount { get; private set; }

        public ValueTask<AccessTokenRefreshResponse> RefreshUserAccessToken(
            ClientIdentity client,
            RefreshToken refreshToken,
            CancellationToken ct = default)
        {
            RefreshCount++;
            return ValueTask.FromResult(new AccessTokenRefreshResponse
            {
                AccessToken = new UserAccessToken(newAccessToken),
                RefreshToken = new RefreshToken(TokenResolutionTestFixture.TestNewRefreshToken),
                ExpiresIn = TimeSpan.FromHours(4),
                TokenType = "bearer",
                Scope = [Scope.ChannelModerate]
            });
        }
    }

    private class MockDelayedTokenRefresher(string newAccessToken, TimeSpan delay) : IRefreshUserAccessToken
    {
        private int _refreshCount;
        public int RefreshCount => _refreshCount;

        public async ValueTask<AccessTokenRefreshResponse> RefreshUserAccessToken(
            ClientIdentity client,
            RefreshToken refreshToken,
            CancellationToken ct = default)
        {
            Interlocked.Increment(ref _refreshCount);
            await Task.Delay(delay, ct);
            return new AccessTokenRefreshResponse
            {
                AccessToken = new UserAccessToken(newAccessToken),
                RefreshToken = new RefreshToken(TokenResolutionTestFixture.TestNewRefreshToken),
                ExpiresIn = TimeSpan.FromHours(4),
                TokenType = "bearer",
                Scope = [Scope.ChannelModerate]
            };
        }
    }

    private class MockFailingTokenRefresher : IRefreshUserAccessToken
    {
        public ValueTask<AccessTokenRefreshResponse> RefreshUserAccessToken(
            ClientIdentity client,
            RefreshToken refreshToken,
            CancellationToken ct = default)
        {
            throw new TwitchApiException("Token refresh failed")
            {
                Request = new MockRequest(),
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Headers = new Dictionary<string, IEnumerable<string>>(),
                ContentHeaders = new Dictionary<string, IEnumerable<string>>(),
                Content = []
            };
        }

        private record MockRequest() : ITwitchRequest
        {
            public HttpRequestMessage ToHttpRequestMessage(System.Text.Json.JsonSerializerOptions serializerOptions) => new();
        }
    }

    #endregion
}
