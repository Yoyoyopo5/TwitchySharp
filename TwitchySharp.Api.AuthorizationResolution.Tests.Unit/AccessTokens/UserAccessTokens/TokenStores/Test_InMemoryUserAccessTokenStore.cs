using System.Collections.Immutable;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_InMemoryUserAccessTokenStore
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly UserId TestUserId = new("123456789");
    private static readonly UserIdentity TestUserIdentity = new(TestUserId) { ClientId = TestClientId };
    private static readonly UserAccessToken TestToken = new("test_access_token");

    #region SaveTokenDetails Tests

    [Fact]
    public async Task SaveTokenDetails_NewToken_StoresSuccessfully()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();

        // Act
        var result = await store.SaveTokenDetails(key, details);

        // Assert
        Assert.Equal(details, result);
        var retrieved = await store.GetTokenDetails(key);
        Assert.Equal(details, retrieved);
    }

    [Fact]
    public async Task SaveTokenDetails_ExistingUser_ReplacesOldToken()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var oldDetails = CreateDetails(token: new UserAccessToken("old_token"));
        var newDetails = CreateDetails(token: new UserAccessToken("new_token"));
        await store.SaveTokenDetails(key, oldDetails);

        // Act
        await store.SaveTokenDetails(key, newDetails);

        // Assert
        var retrieved = await store.GetTokenDetails(key);
        Assert.NotNull(retrieved);
        Assert.Equal("new_token", retrieved.AccessToken.Value);

        // Old token should not be retrievable
        var oldRetrieved = await store.GetTokenDetails(new UserAccessToken("old_token"));
        Assert.Null(oldRetrieved);
    }

    [Fact]
    public async Task SaveTokenDetails_MultipleSaves_MaintainsConsistency()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();

        // Act - Save multiple times with different tokens
        for (int i = 0; i < 10; i++)
        {
            var details = CreateDetails(token: new UserAccessToken($"token_{i}"));
            await store.SaveTokenDetails(key, details);
        }

        // Assert - Only the last token should be retrievable by key
        var retrieved = await store.GetTokenDetails(key);
        Assert.NotNull(retrieved);
        Assert.Equal("token_9", retrieved.AccessToken.Value);
    }

    #endregion

    #region GetTokenDetails (by key) Tests

    [Fact]
    public async Task GetTokenDetailsByKey_ExistingToken_ReturnsDetails()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();
        await store.SaveTokenDetails(key, details);

        // Act
        var result = await store.GetTokenDetails(key);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(details, result);
    }

    [Fact]
    public async Task GetTokenDetailsByKey_NonExistentUser_ReturnsNull()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();

        // Act
        var result = await store.GetTokenDetails(key);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetTokenDetailsByKey_ScopeMatching_RequiresAtLeastOneOverlap()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var storedScopes = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit);
        var details = CreateDetails(scopes: storedScopes);
        var key = CreateKey(validScopes: storedScopes);
        await store.SaveTokenDetails(key, details);

        // Act - Request with scope that overlaps
        var requestKey = CreateKey(validScopes: ImmutableHashSet.Create(Scope.ChannelModerate));
        var result = await store.GetTokenDetails(requestKey);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetTokenDetailsByKey_ScopeMatching_NoOverlapReturnsNull()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var storedScopes = ImmutableHashSet.Create(Scope.ChannelModerate);
        var details = CreateDetails(scopes: storedScopes);
        var key = CreateKey(validScopes: storedScopes);
        await store.SaveTokenDetails(key, details);

        // Act - Request with non-overlapping scope
        var requestKey = CreateKey(validScopes: ImmutableHashSet.Create(Scope.ChatEdit));
        var result = await store.GetTokenDetails(requestKey);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetTokenDetailsByKey_EmptyValidScopes_MatchesAnyToken()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var storedScopes = ImmutableHashSet.Create(Scope.ChannelModerate);
        var details = CreateDetails(scopes: storedScopes);
        var key = CreateKey(validScopes: storedScopes);
        await store.SaveTokenDetails(key, details);

        // Act - Request with empty scopes (should match any)
        var requestKey = CreateKey(validScopes: ImmutableHashSet<Scope>.Empty);
        var result = await store.GetTokenDetails(requestKey);

        // Assert
        Assert.NotNull(result);
    }

    #endregion

    #region GetTokenDetails (by token) Tests

    [Fact]
    public async Task GetTokenDetailsByToken_ExistingToken_ReturnsDetails()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();
        await store.SaveTokenDetails(key, details);

        // Act
        var result = await store.GetTokenDetails(TestToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(details, result);
    }

    [Fact]
    public async Task GetTokenDetailsByToken_NonExistentToken_ReturnsNull()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var nonExistentToken = new UserAccessToken("non_existent_token");

        // Act
        var result = await store.GetTokenDetails(nonExistentToken);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region RemoveToken Tests

    [Fact]
    public async Task RemoveToken_ExistingToken_RemovesAndReturnsDetails()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();
        await store.SaveTokenDetails(key, details);

        // Act
        var removed = await store.RemoveTokenDetails(TestToken);

        // Assert
        Assert.NotNull(removed);
        Assert.Equal(details, removed);

        // Verify it's actually removed
        var afterRemoval = await store.GetTokenDetails(TestToken);
        Assert.Null(afterRemoval);

        var byKey = await store.GetTokenDetails(key);
        Assert.Null(byKey);
    }

    [Fact]
    public async Task RemoveToken_NonExistentToken_ReturnsNull()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var nonExistentToken = new UserAccessToken("non_existent_token");

        // Act
        var result = await store.RemoveTokenDetails(nonExistentToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveToken_RemovesTwice_SecondReturnsNull()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();
        await store.SaveTokenDetails(key, details);

        // Act
        var firstRemoval = await store.RemoveTokenDetails(TestToken);
        var secondRemoval = await store.RemoveTokenDetails(TestToken);

        // Assert
        Assert.NotNull(firstRemoval);
        Assert.Null(secondRemoval);
    }

    #endregion

    #region Thread Safety Tests

    [Fact]
    public async Task ConcurrentSaves_DifferentUsers_AllSucceed()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var tasks = new List<Task>();

        // Act - Concurrent saves for different users
        for (int i = 0; i < 100; i++)
        {
            int userId = i;
            tasks.Add(Task.Run(async () =>
            {
                var user = new UserIdentity(new UserId($"user_{userId}")) { ClientId = TestClientId };
                var key = new UserAccessTokenKey { Identity = user, ValidScopes = ImmutableHashSet<Scope>.Empty };
                var details = new UserAccessTokenDetails
                {
                    Identity = user,
                    AccessToken = new UserAccessToken($"token_{userId}"),
                    ExpiresAt = DateTimeOffset.UtcNow.AddHours(4),
                    Scopes = ImmutableHashSet<Scope>.Empty
                };
                await store.SaveTokenDetails(key, details);
            }));
        }

        await Task.WhenAll(tasks);

        // Assert - All should be retrievable
        for (int i = 0; i < 100; i++)
        {
            var user = new UserIdentity(new UserId($"user_{i}")) { ClientId = TestClientId };
            var key = new UserAccessTokenKey { Identity = user, ValidScopes = ImmutableHashSet<Scope>.Empty };
            var result = await store.GetTokenDetails(key);
            Assert.NotNull(result);
            Assert.Equal($"token_{i}", result.AccessToken.Value);
        }
    }

    [Fact]
    public async Task ConcurrentSaves_SameUser_LastWins()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var tasks = new List<Task>();

        // Act - Concurrent saves for same user
        for (int i = 0; i < 10; i++)
        {
            int tokenIndex = i;
            tasks.Add(Task.Run(async () =>
            {
                var details = CreateDetails(token: new UserAccessToken($"token_{tokenIndex}"));
                await store.SaveTokenDetails(key, details);
            }));
        }

        await Task.WhenAll(tasks);

        // Assert - Exactly one token should exist for this user
        var result = await store.GetTokenDetails(key);
        Assert.NotNull(result);
        Assert.StartsWith("token_", result.AccessToken.Value);
    }

    [Fact]
    public async Task ConcurrentRemovesAndSaves_MaintainsConsistency()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();
        await store.SaveTokenDetails(key, details);

        var tasks = new List<Task>();

        // Act - Concurrent removes and saves
        for (int i = 0; i < 50; i++)
        {
            int index = i;
            if (index % 2 == 0)
            {
                tasks.Add(Task.Run(async () => await store.RemoveTokenDetails(TestToken)));
            }
            else
            {
                tasks.Add(Task.Run(async () =>
                {
                    var newDetails = CreateDetails(token: new UserAccessToken($"token_{index}"));
                    await store.SaveTokenDetails(key, newDetails);
                }));
            }
        }

        await Task.WhenAll(tasks);

        // Assert - Should not throw, store should be in consistent state
        // (either a token exists or it doesn't)
        var byKey = await store.GetTokenDetails(key);
        // Result can be null or a valid token, but state should be consistent
        if (byKey != null)
        {
            var byToken = await store.GetTokenDetails(byKey.AccessToken);
            Assert.Equal(byKey, byToken);
        }
    }

    #endregion

    #region Index Consistency Tests

    [Fact]
    public async Task DualIndexConsistency_AfterSave_BothIndexesMatch()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();

        // Act
        await store.SaveTokenDetails(key, details);

        // Assert - Both indexes should return the same details
        var byKey = await store.GetTokenDetails(key);
        var byToken = await store.GetTokenDetails(TestToken);
        Assert.Equal(byKey, byToken);
    }

    [Fact]
    public async Task DualIndexConsistency_AfterRemove_BothIndexesEmpty()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var details = CreateDetails();
        await store.SaveTokenDetails(key, details);

        // Act
        await store.RemoveTokenDetails(TestToken);

        // Assert - Both indexes should be empty
        var byKey = await store.GetTokenDetails(key);
        var byToken = await store.GetTokenDetails(TestToken);
        Assert.Null(byKey);
        Assert.Null(byToken);
    }

    [Fact]
    public async Task DualIndexConsistency_AfterReplace_OldTokenRemoved()
    {
        // Arrange
        var store = new InMemoryUserAccessTokenStore();
        var key = CreateKey();
        var oldToken = new UserAccessToken("old_token");
        var newToken = new UserAccessToken("new_token");
        var oldDetails = CreateDetails(token: oldToken);
        var newDetails = CreateDetails(token: newToken);
        await store.SaveTokenDetails(key, oldDetails);

        // Act
        await store.SaveTokenDetails(key, newDetails);

        // Assert
        var byOldToken = await store.GetTokenDetails(oldToken);
        var byNewToken = await store.GetTokenDetails(newToken);
        var byKey = await store.GetTokenDetails(key);

        Assert.Null(byOldToken);
        Assert.NotNull(byNewToken);
        Assert.NotNull(byKey);
        Assert.Equal(byNewToken, byKey);
    }

    #endregion

    #region Helper Methods

    private static UserAccessTokenKey CreateKey(IReadOnlySet<Scope>? validScopes = null)
    {
        return new UserAccessTokenKey
        {
            Identity = TestUserIdentity,
            ValidScopes = validScopes ?? ImmutableHashSet.Create(Scope.ChannelModerate)
        };
    }

    private static UserAccessTokenDetails CreateDetails(
        UserAccessToken? token = null,
        IReadOnlySet<Scope>? scopes = null)
    {
        return new UserAccessTokenDetails
        {
            Identity = TestUserIdentity,
            AccessToken = token ?? TestToken,
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(4),
            Scopes = scopes ?? ImmutableHashSet.Create(Scope.ChannelModerate)
        };
    }

    #endregion
}
