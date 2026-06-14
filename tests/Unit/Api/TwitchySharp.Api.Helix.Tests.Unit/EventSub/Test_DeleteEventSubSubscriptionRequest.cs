using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub;

namespace TwitchySharp.Api.Tests.Unit.Helix.EventSub;

public class Test_DeleteEventSubSubscriptionRequest
{
    private const string MOCK_USER_ID = "12345";
    private const string MOCK_BROADCASTER_ID = "99999";
    private const string MOCK_SUBSCRIPTION_ID = "sub_123";

    [Fact]
    public void DefaultIdentity_WithWebsocketUserAuthorizedSubscription_ReturnsUserIdentity()
    {
        EventSubSubscription subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<ConditionKey, string>
            {
                [new("broadcaster_user_id")] = MOCK_BROADCASTER_ID,
                [new("moderator_user_id")] = MOCK_USER_ID
            },
            EventSubTransportMethod.Websocket
        );
        DeleteEventSubSubscriptionRequest request = new(subscription);

        TwitchIdentity identity = request.AuthorizationContext.Identity;

        TwitchIdentity.User userIdentity = Assert.IsType<TwitchIdentity.User>(identity);
        Assert.Equal(new UserId(MOCK_USER_ID), userIdentity.UserId);
    }

    [Fact]
    public void DefaultIdentity_WithWebhookSubscription_ReturnsDefault()
    {
        EventSubSubscription subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<ConditionKey, string>
            {
                [new("broadcaster_user_id")] = MOCK_BROADCASTER_ID,
                [new("moderator_user_id")] = MOCK_USER_ID
            },
            EventSubTransportMethod.Webhook
        );
        DeleteEventSubSubscriptionRequest request = new(subscription);

        TwitchIdentity identity = request.AuthorizationContext.Identity;

        Assert.Equal(TwitchIdentity.Client.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithConduitSubscription_ReturnsDefault()
    {
        EventSubSubscription subscription = CreateSubscription(
            EventSubSubscriptionType.StreamOnline,
            new Dictionary<ConditionKey, string>
            {
                [new("broadcaster_user_id")] = MOCK_BROADCASTER_ID
            },
            EventSubTransportMethod.Conduit
        );
        DeleteEventSubSubscriptionRequest request = new(subscription);

        TwitchIdentity identity = request.AuthorizationContext.Identity;

        Assert.Equal(TwitchIdentity.Client.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithSubscriptionIdOnly_ReturnsDefault()
    {
        DeleteEventSubSubscriptionRequest request = new()
        {
            SubscriptionId = new EventSubSubscriptionId(MOCK_SUBSCRIPTION_ID)
        };

        TwitchIdentity identity = request.AuthorizationContext.Identity;

        Assert.Equal(TwitchIdentity.Client.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithWebsocketUserAuthorizedMissingCondition_ThrowsInvalidOperationException()
    {
        EventSubSubscription subscription = CreateSubscription(
            EventSubSubscriptionType.ChannelFollow,
            new Dictionary<ConditionKey, string>
            {
                [new("broadcaster_user_id")] = MOCK_BROADCASTER_ID
                // moderator_user_id is missing
            },
            EventSubTransportMethod.Websocket
        );
        DeleteEventSubSubscriptionRequest request = new(subscription);

        Assert.Throws<InvalidOperationException>(() => request.AuthorizationContext.Identity);
    }

    [Fact]
    public void Constructor_WithSubscription_SetsSubscriptionId()
    {
        EventSubSubscription subscription = CreateSubscription(
            EventSubSubscriptionType.StreamOnline,
            new Dictionary<ConditionKey, string>
            {
                [new("broadcaster_user_id")] = MOCK_BROADCASTER_ID
            },
            EventSubTransportMethod.Webhook
        );

        DeleteEventSubSubscriptionRequest request = new(subscription);

        Assert.Equal(subscription.Id, request.SubscriptionId);
    }

    private static EventSubSubscription CreateSubscription(
        EventSubSubscriptionType type,
        Dictionary<ConditionKey, string> condition,
        EventSubTransportMethod transportMethod)
    {
        return new EventSubSubscription
        {
            Id = new EventSubSubscriptionId(MOCK_SUBSCRIPTION_ID),
            Status = EventSubSubscriptionStatus.Enabled,
            Type = new EventSubSubscriptionTypeName(type.Type),
            Version = new EventSubSubscriptionTypeVersion(type.Version),
            Condition = condition.ToImmutableDictionary(),
            CreatedAt = DateTimeOffset.UtcNow,
            Transport = new EventSubSubscriptionTransport
            {
                Method = transportMethod
            },
            Cost = 1
        };
    }
}
