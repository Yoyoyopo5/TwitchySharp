using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution.Tests.Integration.Fixtures;

/// <summary>
/// Test fixture providing test data and helpers for authorization resolution integration tests.
/// </summary>
public class TokenResolutionTestFixture
{
    // Test constants
    public const string TestClientId = "test_client_id";
    public const string TestClientSecret = "test_client_secret";
    public const string TestAccessToken = "test_access_token";
    public const string TestRefreshToken = "test_refresh_token";
    public const string TestUserId = "123456789";
    public const string TestNewAccessToken = "new_access_token";
    public const string TestNewRefreshToken = "new_refresh_token";

    public static readonly ClientId ClientId = new(TestClientId);
    public static readonly ClientSecret ClientSecret = new(TestClientSecret);
    public static readonly ClientIdentity ClientIdentity = new(ClientId);
    public static readonly UserIdentity TestUserIdentity = new(new UserId(TestUserId)) { ClientId = ClientId };
    public static readonly UserAccessToken AccessToken = new(TestAccessToken);
    public static readonly RefreshToken RefreshToken = new(TestRefreshToken);

    /// <summary>
    /// Creates a default request authorizer with test configuration.
    /// </summary>
    public DefaultRequestAuthorizer CreateDefaultAuthorizer() =>
        new(
            new DefaultClientIdentityResolver(ClientIdentity),
            new SingleAccessTokenResolver(AccessToken)
        );

    /// <summary>
    /// Creates a user access token store pre-populated with test data.
    /// </summary>
    public InMemoryUserAccessTokenStore CreatePopulatedTokenStore(
        UserAccessTokenDetails? details = null,
        UserAccessTokenKey? key = null)
    {
        var store = new InMemoryUserAccessTokenStore();
        details ??= CreateTestTokenDetails();
        key ??= CreateTestTokenKey();
        store.SaveTokenDetails(key, details);
        return store;
    }

    /// <summary>
    /// Creates test user access token details.
    /// </summary>
    public UserAccessTokenDetails CreateTestTokenDetails(
        DateTimeOffset? expiresAt = null,
        RefreshToken? refreshToken = null,
        IReadOnlySet<Scope>? scopes = null)
    {
        return new UserAccessTokenDetails
        {
            Identity = TestUserIdentity,
            AccessToken = AccessToken,
            RefreshToken = refreshToken ?? RefreshToken,
            ExpiresAt = expiresAt ?? DateTimeOffset.UtcNow.AddHours(4),
            Scopes = scopes ?? ImmutableHashSet.Create(Scope.ChannelModerate)
        };
    }

    /// <summary>
    /// Creates a test user access token key.
    /// </summary>
    public UserAccessTokenKey CreateTestTokenKey(IReadOnlySet<Scope>? validScopes = null)
    {
        return new UserAccessTokenKey
        {
            User = TestUserIdentity,
            ValidScopes = validScopes ?? ImmutableHashSet.Create(Scope.ChannelModerate)
        };
    }
}
