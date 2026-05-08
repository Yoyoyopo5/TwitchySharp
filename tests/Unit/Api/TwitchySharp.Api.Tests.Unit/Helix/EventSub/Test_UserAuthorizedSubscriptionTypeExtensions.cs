using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;

namespace TwitchySharp.Api.Tests.Unit.Helix.EventSub;

public class Test_UserAuthorizedSubscriptionTypeExtensions
{
    private const string MOCK_USER_ID = "12345";
    private const string MOCK_BROADCASTER_ID = "99999";

    [Fact]
    public void GetAuthorizingUser_WithValidCondition_ReturnsUserIdentity()
    {
        // Arrange - ChannelFollow has moderator_user_id as the authorizing user
        var subscriptionType = new ChannelFollow(
            BroadcasterUserId: new UserId(MOCK_BROADCASTER_ID),
            ModeratorUserId: new UserId(MOCK_USER_ID)
        );

        // Act
        var result = subscriptionType.GetAuthorizingUser();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new UserId(MOCK_USER_ID), result.UserId);
    }

    [Fact]
    public void GetAuthorizingUser_WithMissingConditionKey_ReturnsNull()
    {
        // Arrange - A mock type that claims to have a condition key that doesn't exist
        var subscriptionType = new MockUserAuthorizedTypeWithMissingKey();

        // Act
        var result = subscriptionType.GetAuthorizingUser();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAuthorizingUser_WithNonUserIdConditionValue_ReturnsNull()
    {
        // Arrange - A mock type where the condition value is not a UserId
        var subscriptionType = new MockUserAuthorizedTypeWithWrongValueType();

        // Act
        var result = subscriptionType.GetAuthorizingUser();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAuthorizingUser_WithBroadcasterAuthorizedType_ReturnsCorrectUser()
    {
        // Arrange - ChannelSubscribe uses broadcaster_user_id as the authorizing user
        var subscriptionType = new ChannelSubscribe(new UserId(MOCK_USER_ID));

        // Act
        var result = subscriptionType.GetAuthorizingUser();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new UserId(MOCK_USER_ID), result.UserId);
    }

    /// <summary>
    /// Mock type where the authorizing user condition key doesn't exist in the condition.
    /// </summary>
    private sealed record MockUserAuthorizedTypeWithMissingKey : IUserAuthorizedSubscriptionType
    {
        public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelFollow;
        public ConditionKey AuthorizingUserConditionKey => new ConditionKey("nonexistent_key");
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorReadFollowers);

        public IReadOnlyDictionary<ConditionKey, object> Condition => new Dictionary<ConditionKey, object>
        {
            [new ConditionKey("broadcaster_user_id")] = new UserId(MOCK_BROADCASTER_ID)
        };
    }

    /// <summary>
    /// Mock type where the condition value is not a UserId.
    /// </summary>
    private sealed record MockUserAuthorizedTypeWithWrongValueType : IUserAuthorizedSubscriptionType
    {
        public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelFollow;
        public ConditionKey AuthorizingUserConditionKey => new ConditionKey("user_id");
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorReadFollowers);

        public IReadOnlyDictionary<ConditionKey, object> Condition => new Dictionary<ConditionKey, object>
        {
            // Value is a string, not a UserId
            [new ConditionKey("user_id")] = "not_a_user_id_type"
        };
    }
}
