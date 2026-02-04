using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Helix.EventSub;

public class Test_CreateEventSubSubscriptionRequest
{
    private const string MOCK_USER_ID = "12345";
    private const string MOCK_BROADCASTER_ID = "99999";
    private const string MOCK_SESSION_ID = "session_abc";

    [Fact]
    public void DefaultIdentity_WithWebsocketUserAuthorizedType_ReturnsUserIdentity()
    {
        // Arrange
        var subscriptionType = new ChannelFollow(
            BroadcasterUserId: new UserId(MOCK_BROADCASTER_ID),
            ModeratorUserId: new UserId(MOCK_USER_ID)
        );
        var transport = new WebsocketSubscriptionTransport(new EventSubWebsocketSessionId(MOCK_SESSION_ID));
        var request = new CreateEventSubSubscriptionRequest
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = transport
            }
        };

        // Act
        var identity = GetDefaultIdentity(request);

        // Assert
        var userIdentity = Assert.IsType<UserIdentity>(identity);
        Assert.Equal(new UserId(MOCK_USER_ID), userIdentity.UserId);
    }

    [Fact]
    public void DefaultIdentity_WithWebhookUserAuthorizedType_ReturnsDefault()
    {
        // Arrange - Webhook transport with user authorized type should use app identity
        var subscriptionType = new ChannelFollow(
            BroadcasterUserId: new UserId(MOCK_BROADCASTER_ID),
            ModeratorUserId: new UserId(MOCK_USER_ID)
        );
        var transport = new WebhookSubscriptionTransport(
            callback: new Uri("https://example.com/webhook"),
            secret: "test_secret_123"
        );
        var request = new CreateEventSubSubscriptionRequest
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = transport
            }
        };

        // Act
        var identity = GetDefaultIdentity(request);

        // Assert
        Assert.Equal(TwitchApiIdentity.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithWebsocketAppOnlyType_ReturnsDefault()
    {
        // Arrange - App-only subscription type with websocket transport
        var subscriptionType = new StreamOnline(new UserId(MOCK_BROADCASTER_ID));
        var transport = new WebsocketSubscriptionTransport(new EventSubWebsocketSessionId(MOCK_SESSION_ID));
        var request = new CreateEventSubSubscriptionRequest
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = transport
            }
        };

        // Act
        var identity = GetDefaultIdentity(request);

        // Assert
        Assert.Equal(TwitchApiIdentity.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithWebhookAppOnlyType_ReturnsDefault()
    {
        // Arrange
        var subscriptionType = new StreamOnline(new UserId(MOCK_BROADCASTER_ID));
        var transport = new WebhookSubscriptionTransport(
            callback: new Uri("https://example.com/webhook"),
            secret: "test_secret_123"
        );
        var request = new CreateEventSubSubscriptionRequest
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = transport
            }
        };

        // Act
        var identity = GetDefaultIdentity(request);

        // Assert
        Assert.Equal(TwitchApiIdentity.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithWebsocketUserAuthorizedTypeMissingCondition_ThrowsInvalidOperationException()
    {
        // Arrange - User authorized type missing the authorizing user condition
        var subscriptionType = new MockUserAuthorizedTypeWithMissingCondition();
        var transport = new WebsocketSubscriptionTransport(new EventSubWebsocketSessionId(MOCK_SESSION_ID));
        var request = new CreateEventSubSubscriptionRequest
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = transport
            }
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => GetDefaultIdentity(request));
    }

    [Fact]
    public void ValidScopes_WithUserAuthorizedType_ReturnsTypeScopes()
    {
        // Arrange
        var subscriptionType = new ChannelFollow(
            BroadcasterUserId: new UserId(MOCK_BROADCASTER_ID),
            ModeratorUserId: new UserId(MOCK_USER_ID)
        );
        var transport = new WebsocketSubscriptionTransport(new EventSubWebsocketSessionId(MOCK_SESSION_ID));
        var request = new CreateEventSubSubscriptionRequest
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = transport
            }
        };

        // Act
        var scopes = request.ValidScopes;

        // Assert
        Assert.Contains(Scope.ModeratorReadFollowers, scopes);
    }

    [Fact]
    public void ValidScopes_WithAppOnlyType_ReturnsEmpty()
    {
        // Arrange
        var subscriptionType = new StreamOnline(new UserId(MOCK_BROADCASTER_ID));
        var transport = new WebhookSubscriptionTransport(
            callback: new Uri("https://example.com/webhook"),
            secret: "test_secret_123"
        );
        var request = new CreateEventSubSubscriptionRequest
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = transport
            }
        };

        // Act
        var scopes = request.ValidScopes;

        // Assert
        Assert.Empty(scopes);
    }

    /// <summary>
    /// Helper to access the protected DefaultIdentity property through the public interface.
    /// </summary>
    private static TwitchApiIdentity GetDefaultIdentity(CreateEventSubSubscriptionRequest request)
    {
        // The Identity property falls back to DefaultIdentity when not set
        // We can test this by not setting Identity and checking what Identity resolves to
        return request.Identity;
    }

    /// <summary>
    /// A mock user-authorized subscription type that is missing the authorizing user condition.
    /// </summary>
    private sealed record MockUserAuthorizedTypeWithMissingCondition : IUserAuthorizedSubscriptionType
    {
        public EventSubSubscriptionType Type => EventSubSubscriptionType.ChannelFollow;
        public ConditionKey AuthorizingUserConditionKey => new ConditionKey("moderator_user_id");
        public IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ModeratorReadFollowers);

        // Condition is missing the moderator_user_id key
        public IReadOnlyDictionary<ConditionKey, object> Condition => new Dictionary<ConditionKey, object>
        {
            [new ConditionKey("broadcaster_user_id")] = new UserId(MOCK_BROADCASTER_ID)
        };
    }
}
