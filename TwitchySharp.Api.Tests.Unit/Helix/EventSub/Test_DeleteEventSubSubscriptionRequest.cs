using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.EventSub;

public class Test_DeleteEventSubSubscriptionRequest
{
    private const string MOCK_USER_ID = "12345";
    private const string MOCK_BROADCASTER_ID = "99999";
    private const string MOCK_SUBSCRIPTION_ID = "sub_123";

    [Fact]
    public void DefaultIdentity_WithWebsocketUserAuthorizedSubscription_ReturnsUserIdentity()
    {
        // Arrange - ChannelFollow with websocket transport
        var subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = MOCK_BROADCASTER_ID,
                ["moderator_user_id"] = MOCK_USER_ID
            },
            EventSubTransportMethod.Websocket
        );
        var request = new DeleteEventSubSubscriptionRequest(subscription);

        // Act
        var identity = request.Identity;

        // Assert
        var userIdentity = Assert.IsType<UserIdentity>(identity);
        Assert.Equal(new UserId(MOCK_USER_ID), userIdentity.UserId);
    }

    [Fact]
    public void DefaultIdentity_WithWebhookSubscription_ReturnsDefault()
    {
        // Arrange - Webhook transport should use app identity
        var subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = MOCK_BROADCASTER_ID,
                ["moderator_user_id"] = MOCK_USER_ID
            },
            EventSubTransportMethod.Webhook
        );
        var request = new DeleteEventSubSubscriptionRequest(subscription);

        // Act
        var identity = request.Identity;

        // Assert
        Assert.Equal(TwitchApiIdentity.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithConduitSubscription_ReturnsDefault()
    {
        // Arrange - Conduit transport should use app identity
        var subscription = CreateSubscription(
            EventSubSubscriptionType.StreamOnline,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = MOCK_BROADCASTER_ID
            },
            EventSubTransportMethod.Conduit
        );
        var request = new DeleteEventSubSubscriptionRequest(subscription);

        // Act
        var identity = request.Identity;

        // Assert
        Assert.Equal(TwitchApiIdentity.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithSubscriptionIdOnly_ReturnsDefault()
    {
        // Arrange - No subscription provided, only ID
        var request = new DeleteEventSubSubscriptionRequest
        {
            SubscriptionId = new EventSubSubscriptionId(MOCK_SUBSCRIPTION_ID)
        };

        // Act
        var identity = request.Identity;

        // Assert
        Assert.Equal(TwitchApiIdentity.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithWebsocketUserAuthorizedMissingCondition_ThrowsInvalidOperationException()
    {
        // Arrange - User authorized type with websocket but missing the authorizing user condition
        var subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = MOCK_BROADCASTER_ID
                // moderator_user_id is missing
            },
            EventSubTransportMethod.Websocket
        );
        var request = new DeleteEventSubSubscriptionRequest(subscription);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => request.Identity);
    }

    [Fact]
    public void Constructor_WithSubscription_SetsSubscriptionId()
    {
        // Arrange
        var subscription = CreateSubscription(
            EventSubSubscriptionType.StreamOnline,
            new Dictionary<string, string>
            {
                ["broadcaster_user_id"] = MOCK_BROADCASTER_ID
            },
            EventSubTransportMethod.Webhook
        );

        // Act
        var request = new DeleteEventSubSubscriptionRequest(subscription);

        // Assert
        Assert.Equal(subscription.Id, request.SubscriptionId);
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
