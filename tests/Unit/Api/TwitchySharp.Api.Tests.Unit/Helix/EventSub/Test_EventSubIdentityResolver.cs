using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub;

namespace TwitchySharp.Api.Tests.Unit.Helix.EventSub;

public class Test_EventSubIdentityResolver
{
    private const string MOCK_USER_ID = "12345";
    private const string MOCK_SUBSCRIPTION_ID = "sub_123";

    [Fact]
    public void GetAuthorizingUser_WithUserAuthorizedType_ReturnsUserIdentity()
    {
        // Arrange - ChannelFollow uses moderator_user_id as the authorizing user key
        var subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = "999",
                ["moderator_user_id"] = MOCK_USER_ID
            },
            EventSubTransportMethod.Websocket
        );

        // Act
        var result = subscription.GetAuthorizingUser();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new UserId(MOCK_USER_ID), result.UserId);
    }

    [Fact]
    public void GetAuthorizingUser_WithAppOnlyType_ReturnsNull()
    {
        // Arrange - StreamOnline does not require user authorization
        var subscription = CreateSubscription(
            EventSubSubscriptionType.StreamOnline,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = MOCK_USER_ID
            },
            EventSubTransportMethod.Webhook
        );

        // Act
        var result = subscription.GetAuthorizingUser();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAuthorizingUser_WithMissingConditionKey_ReturnsNull()
    {
        // Arrange - ChannelFollow requires moderator_user_id but it's missing
        var subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = "999"
                // moderator_user_id is missing
            },
            EventSubTransportMethod.Websocket
        );

        // Act
        var result = subscription.GetAuthorizingUser();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAuthorizingUser_WithWebhookTransportUserAuthorizedType_ReturnsUserIdentity()
    {
        // Arrange - Even with webhook transport, the authorizing user should be resolved
        var subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = "999",
                ["moderator_user_id"] = MOCK_USER_ID
            },
            EventSubTransportMethod.Webhook
        );

        // Act
        var result = subscription.GetAuthorizingUser();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new UserId(MOCK_USER_ID), result.UserId);
    }

    [Fact]
    public void GetAuthorizingUser_WithBroadcasterAuthorizedType_ReturnsCorrectUser()
    {
        // Arrange - ChannelSubscribe uses broadcaster_user_id as the authorizing user key
        var subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelSubscribe,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = MOCK_USER_ID
            },
            EventSubTransportMethod.Websocket
        );

        // Act
        var result = subscription.GetAuthorizingUser();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(new UserId(MOCK_USER_ID), result.UserId);
    }

    [Fact]
    public void GetAuthorizingUser_WithUnknownSubscriptionType_ReturnsNull()
    {
        // Arrange - Unknown/custom subscription type not in the dictionary
        var subscription = CreateSubscription(
            new EventSubSubscriptionType("unknown.type", "1"),
            new Dictionary<string, string>
            {
                ["user_id"] = MOCK_USER_ID
            },
            EventSubTransportMethod.Websocket
        );

        // Act
        var result = subscription.GetAuthorizingUser();

        // Assert
        Assert.Null(result);
    }

    private static EventSubSubscription CreateSubscription(
        EventSubSubscriptionType type,
        Dictionary<string, string> condition,
        EventSubTransportMethod transportMethod)
    {
        return new EventSubSubscription
        {
            Id = new EventSubSubscriptionId(MOCK_SUBSCRIPTION_ID),
            Status = EventSubSubscriptionStatus.Enabled,
            Type = new EventSubSubscriptionTypeName(type.Type),
            Version = new EventSubSubscriptionTypeVersion(type.Version),
            Condition = condition.ToImmutableDictionary(
                kvp => new ConditionKey(kvp.Key),
                kvp => kvp.Value),
            CreatedAt = DateTimeOffset.UtcNow,
            Transport = new EventSubSubscriptionTransport
            {
                Method = transportMethod
            },
            Cost = 1
        };
    }
}
