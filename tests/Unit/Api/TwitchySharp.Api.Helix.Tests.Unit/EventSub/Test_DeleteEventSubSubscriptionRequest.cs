using System.Collections.Immutable;
using TwitchySharp.Api.Helix.EventSub;

namespace TwitchySharp.Api.Tests.Unit.Helix.EventSub;

public class Test_DeleteEventSubSubscriptionRequest
{
    private const string MOCK_USER_ID = "12345";
    private const string MOCK_BROADCASTER_ID = "99999";
    private const string MOCK_SUBSCRIPTION_ID = "sub_123";

    [Fact]
    public void AuthenticationContext_WithWebsocketUserAuthorizedSubscription_IsUserAuthenticationContext()
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

        ITwitchRequestAuthenticationContext<TwitchIdentity.User> context = Assert.IsType<ITwitchRequestAuthenticationContext<TwitchIdentity.User>>(request.AuthenticationContext, false);
        Assert.Equal(new UserId(MOCK_USER_ID), context.Identity.UserId);
    }

    [Fact]
    public void AuthenticationContextTokenType_WithWebhookSubscription_IsApp()
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

        Assert.Equal(BearerTokenType.AppAccessToken, request.AuthenticationContext.TokenType);
    }

    [Fact]
    public void AuthenticationContextTokenType_WithConduitSubscription_IsApp()
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

        Assert.Equal(BearerTokenType.AppAccessToken, request.AuthenticationContext.TokenType);
    }

    [Fact]
    public void AuthenticationContext_WithSubscriptionIdOnly_IsDefaultContext()
    {
        DeleteEventSubSubscriptionRequest request = new()
        {
            SubscriptionId = new EventSubSubscriptionId(MOCK_SUBSCRIPTION_ID)
        };

        Assert.Equal(TwitchRequestAuthenticationContext.Default, request.AuthenticationContext);
    }

    [Fact]
    public void AuthenticationContext_WithWebsocketUserAuthorizedMissingCondition_ThrowsInvalidOperationException()
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

        Assert.Throws<InvalidOperationException>(() => request.AuthenticationContext);
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
