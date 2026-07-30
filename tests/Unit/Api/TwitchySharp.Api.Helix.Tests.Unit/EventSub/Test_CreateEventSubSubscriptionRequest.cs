using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;

namespace TwitchySharp.Api.Tests.Unit.Helix.EventSub;

public class Test_CreateEventSubSubscriptionRequest
{
    private const string FAKE_USER_ID = "12345";
    private const string FAKE_BROADCASTER_ID = "99999";
    private const string FAKE_SESSION_ID = "session_abc";

    private static readonly WebsocketSubscriptionTransport FakeWebsocketTransport = new(new EventSubWebsocketSessionId(FAKE_SESSION_ID));

    private const string FAKE_WEBHOOK_SECRET = "12345";
    private const string FAKE_WEBHOOK_CALLBACK = "https://example.com/webhook";
    private static readonly WebhookSubscriptionTransport FakeWebhookTransport = new(
            callback: new(FAKE_WEBHOOK_CALLBACK),
            secret: new(FAKE_WEBHOOK_SECRET)
        );

    [Fact]
    public void DefaultIdentity_WithWebsocketUserAuthorizedType_ReturnsUserIdentity()
    {
        ChannelFollow subscriptionType = new(
            BroadcasterUserId: new UserId(FAKE_BROADCASTER_ID),
            ModeratorUserId: new UserId(FAKE_USER_ID)
        );
        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new()
            {
                Type = subscriptionType,
                Transport = FakeWebsocketTransport
            }
        };

        TwitchIdentity identity = GetDefaultIdentity(request);

        TwitchIdentity.User userIdentity = Assert.IsType<TwitchIdentity.User>(identity);
        Assert.Equal(new UserId(FAKE_USER_ID), userIdentity.UserId);
    }

    [Fact]
    public void DefaultIdentity_WithWebhookUserAuthorizedType_ReturnsDefault()
    {
        ChannelFollow subscriptionType = new(
            BroadcasterUserId: new UserId(FAKE_BROADCASTER_ID),
            ModeratorUserId: new UserId(FAKE_USER_ID)
        );
        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new()
            {
                Type = subscriptionType,
                Transport = FakeWebhookTransport
            }
        };

        TwitchIdentity identity = GetDefaultIdentity(request);

        Assert.Equal(TwitchIdentity.Client.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithWebsocketAppOnlyType_ReturnsDefault()
    {
        StreamOnline subscriptionType = new(new UserId(FAKE_BROADCASTER_ID));
        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = FakeWebsocketTransport
            }
        };

        TwitchIdentity identity = GetDefaultIdentity(request);

        Assert.Equal(TwitchIdentity.Client.Default, identity);
    }

    [Fact]
    public void DefaultIdentity_WithWebhookAppOnlyType_ReturnsDefault()
    {
        StreamOnline subscriptionType = new(new UserId(FAKE_BROADCASTER_ID));
        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = FakeWebhookTransport
            }
        };

        TwitchIdentity identity = GetDefaultIdentity(request);

        Assert.Equal(TwitchIdentity.Client.Default, identity);
    }

    [Fact]
    public void ValidScopes_WithUserAuthorizedType_ReturnsTypeScopes()
    {
        ChannelFollow subscriptionType = new(
            BroadcasterUserId: new UserId(FAKE_BROADCASTER_ID),
            ModeratorUserId: new UserId(FAKE_USER_ID)
        );
        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = FakeWebsocketTransport
            }
        };

        IEnumerable<Scope> scopes = request.AuthorizationContext.ValidScopes;

        Assert.Contains(Scope.ModeratorReadFollowers, scopes);
    }

    [Fact]
    public void ValidScopes_WithAppOnlyType_ReturnsEmpty()
    {
        StreamOnline subscriptionType = new(new UserId(FAKE_BROADCASTER_ID));
        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new EventSubSubscriptionSpecification
            {
                Type = subscriptionType,
                Transport = FakeWebhookTransport
            }
        };

        IEnumerable<Scope> scopes = request.AuthorizationContext.ValidScopes;

        Assert.Empty(scopes);
    }

    /// <summary>
    /// Helper to access the protected DefaultIdentity property through the public interface.
    /// </summary>
    private static TwitchIdentity GetDefaultIdentity(CreateEventSubSubscriptionRequest request)
        => request.AuthorizationContext.Identity;
}
