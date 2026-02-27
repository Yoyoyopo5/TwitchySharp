using System.Collections.Immutable;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_UserAccessTokenKey
{
    private static readonly ClientId TestClientId = new("test_client_id");
    private static readonly UserId TestUserId = new("123456789");
    private static readonly UserIdentity TestUserIdentity = new(TestUserId) { ClientId = TestClientId };

    [Fact]
    public void Equals_SameUserAndScopes_ReturnsTrue()
    {
        // Arrange
        var scopes = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes };

        // Act & Assert
        Assert.Equal(key1, key2);
        Assert.True(key1.Equals(key2));
    }

    [Fact]
    public void Equals_SameScopesInDifferentOrder_ReturnsTrue()
    {
        // Arrange
        var scopes1 = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit, Scope.BitsRead);
        var scopes2 = ImmutableHashSet.Create(Scope.BitsRead, Scope.ChannelModerate, Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes1 };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes2 };

        // Act & Assert
        Assert.Equal(key1, key2);
    }

    [Fact]
    public void Equals_DifferentScopes_ReturnsFalse()
    {
        // Arrange
        var scopes1 = ImmutableHashSet.Create(Scope.ChannelModerate);
        var scopes2 = ImmutableHashSet.Create(Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes1 };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes2 };

        // Act & Assert
        Assert.NotEqual(key1, key2);
    }

    [Fact]
    public void Equals_DifferentUser_ReturnsFalse()
    {
        // Arrange
        var user2 = new UserIdentity(new UserId("different_user")) { ClientId = TestClientId };
        var scopes = ImmutableHashSet.Create(Scope.ChannelModerate);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes };
        var key2 = new UserAccessTokenKey { Identity = user2, ValidScopes = scopes };

        // Act & Assert
        Assert.NotEqual(key1, key2);
    }

    [Fact]
    public void Equals_EmptyScopes_WorksCorrectly()
    {
        // Arrange
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = ImmutableHashSet<Scope>.Empty };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = ImmutableHashSet<Scope>.Empty };

        // Act & Assert
        Assert.Equal(key1, key2);
    }

    [Fact]
    public void Equals_OneEmptyOneWithScopes_ReturnsFalse()
    {
        // Arrange
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = ImmutableHashSet<Scope>.Empty };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = ImmutableHashSet.Create(Scope.ChannelModerate) };

        // Act & Assert
        Assert.NotEqual(key1, key2);
    }

    [Fact]
    public void GetHashCode_SameUserAndScopes_ReturnsSameHash()
    {
        // Arrange
        var scopes = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes };

        // Act & Assert
        Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ScopesInDifferentOrder_ReturnsSameHash()
    {
        // Arrange
        var scopes1 = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit, Scope.BitsRead);
        var scopes2 = ImmutableHashSet.Create(Scope.BitsRead, Scope.ChannelModerate, Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes1 };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes2 };

        // Act & Assert
        Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentScopes_DifferentHash()
    {
        // Arrange (note: hash collisions are possible but unlikely for different data)
        var scopes1 = ImmutableHashSet.Create(Scope.ChannelModerate);
        var scopes2 = ImmutableHashSet.Create(Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes1 };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes2 };

        // Act & Assert - These should likely be different (though hash collisions are technically possible)
        // This tests the hash function is considering scope content
        Assert.NotEqual(key1.GetHashCode(), key2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_EmptyScopes_WorksCorrectly()
    {
        // Arrange
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = ImmutableHashSet<Scope>.Empty };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = ImmutableHashSet<Scope>.Empty };

        // Act & Assert
        Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
    }

    [Fact]
    public void DictionaryLookup_SameKey_FindsValue()
    {
        // Arrange
        var scopes = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes };
        var dict = new Dictionary<UserAccessTokenKey, string> { { key1, "test_value" } };

        // Act
        var found = dict.TryGetValue(key2, out var value);

        // Assert
        Assert.True(found);
        Assert.Equal("test_value", value);
    }

    [Fact]
    public void DictionaryLookup_ScopesInDifferentOrder_FindsValue()
    {
        // Arrange
        var scopes1 = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit, Scope.BitsRead);
        var scopes2 = ImmutableHashSet.Create(Scope.BitsRead, Scope.ChannelModerate, Scope.ChatEdit);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes1 };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes2 };
        var dict = new Dictionary<UserAccessTokenKey, string> { { key1, "test_value" } };

        // Act
        var found = dict.TryGetValue(key2, out var value);

        // Assert
        Assert.True(found);
        Assert.Equal("test_value", value);
    }

    [Fact]
    public void HashSetContains_SameScopesInDifferentOrder_ReturnsTrue()
    {
        // Arrange
        var scopes1 = ImmutableHashSet.Create(Scope.ChannelModerate, Scope.ChatEdit);
        var scopes2 = ImmutableHashSet.Create(Scope.ChatEdit, Scope.ChannelModerate);
        var key1 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes1 };
        var key2 = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = scopes2 };
        var set = new HashSet<UserAccessTokenKey> { key1 };

        // Act & Assert
        Assert.Contains(key2, set);
    }

    [Fact]
    public void Equals_NullOther_ReturnsFalse()
    {
        // Arrange
        var key = new UserAccessTokenKey { Identity = TestUserIdentity, ValidScopes = ImmutableHashSet<Scope>.Empty };

        // Act & Assert
        Assert.False(key.Equals(null));
    }

    [Fact]
    public void DefaultValidScopes_IsEmptySet()
    {
        // Arrange
        var key = new UserAccessTokenKey { Identity = TestUserIdentity };

        // Act & Assert
        Assert.Empty(key.ValidScopes);
    }
}
