using System.Diagnostics.CodeAnalysis;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public class Test_EventSubSubscriptions(EventSubWebSocketsFixture fixture) : IClassFixture<EventSubWebSocketsFixture>
{
    private readonly EventSubWebSocketsFixture _fixture = fixture;

    [Fact]
    public async Task Send_EventSubWebsocketCreateGetDeleteRequests_ReturnSuccessResponses()
    {
        EventSubWebsocketSessionId sessionId = new(_fixture.SessionId);

        await SendEventSubSubscriptionRequests(
            _fixture,
            EventSubTestProvider.Data.First(static t => t.Data is EventSubTest<ChannelFollow, UserConfiguration>).Data,
            new WebsocketSubscriptionTransport(sessionId),
            TestContext.Current.CancellationToken
            );
    }

    [Theory]
    [ClassData(typeof(EventSubTestProvider))]
    public async Task Send_EventSubWebhookCreateGetDeleteRequests_ReturnSuccessResponses(EventSubTest subscriptionTestData)
    {
        EventSubCallbackUrl callbackUrl = new("https://fake-callback.xyz");
        WebhookSecret secret = new("FAKE_SECRET");

        await SendEventSubSubscriptionRequests(
            _fixture,
            subscriptionTestData,
            new WebhookSubscriptionTransport(callbackUrl, secret),
            TestContext.Current.CancellationToken
            );
    }

    private async static Task SendEventSubSubscriptionRequests(
        EventSubWebSocketsFixture fixture,
        EventSubTest testData,
        EventSubSubscriptionTransportSpecification transport,
        CancellationToken ct
        )
    {
        ITwitchClient client = fixture.GetTwitchApiClient();

        EventSubSubscriptionSpecification specification = new()
        {
            Type = testData.WithFixture(fixture),
            Transport = transport
        };
        EventSubSubscription createdSubscription = await CreateSubscription(client, specification, ct);
        try
        {
            AssertEqual(specification, createdSubscription);

            await Task.Delay(100, ct);

            EventSubSubscription? subscription = await GetSubscription(client, createdSubscription, ct);
            AssertEqual(specification, subscription);
        }
        finally
        {
            await DeleteSubscription(client, createdSubscription, default);
        }
    }

    private static Task<TwitchResponse<DeleteEventSubSubscriptionResponse>> DeleteSubscription(
        ITwitchClient client,
        EventSubSubscription subscription,
        CancellationToken ct
        )
        => client.SendAsync(new DeleteEventSubSubscriptionRequest(subscription), ct);

    private async static Task<EventSubSubscription> GetSubscription(
        ITwitchClient client,
        EventSubSubscription subscription,
        CancellationToken ct
        )
    {
        GetEventSubSubscriptionsRequest requestForSubscription = new() { SubscriptionId = subscription.Id };

        return (await client.SendAsync(
            subscription.Transport.Method != EventSubTransportMethod.Websocket
                ? requestForSubscription
                : new EventSubSubscriptionType(subscription.Type, subscription.Version).GetAuthorizingUserKey() is not ConditionKey userIdConditionKey
                ? throw new KeyNotFoundException($"The authorizing user condition key was not defined for {subscription.Type} {subscription.Version}.")
                : requestForSubscription.ForWebsocketSubscriptions(new(new UserId(subscription.Condition[userIdConditionKey]))),
            ct
            )).Content.Data.Single();
    }



    private async static Task<EventSubSubscription> CreateSubscription(
        ITwitchClient client,
        EventSubSubscriptionSpecification specification,
        CancellationToken ct
        )
        => (await client.SendAsync(new CreateEventSubSubscriptionRequest()
        {
            Subscription = specification
        }, ct)).Content.Data.Single();

    private static void AssertEqual(
        EventSubSubscriptionSpecification specification,
        [NotNull] EventSubSubscription? subscription
        )
    {
        Assert.NotNull(subscription);
        Assert.Equal(specification.Type.Type.Type, subscription.Type.Value);
        Assert.Equal(specification.Type.Type.Version, subscription.Version.Value);
        Assert.True(specification.Type.Condition.All(kvp => subscription.Condition.GetValueOrDefault(kvp.Key) == kvp.Value.ToString()));
        Assert.Equal(specification.Transport.Method, subscription.Transport.Method);
        Assert.Equal(specification.Transport.Callback, subscription.Transport.Callback);
    }
}
