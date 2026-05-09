using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.EventSub;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public class Test_EventSubSubscriptions(EventSubWebSocketsFixture fixture) : IClassFixture<EventSubWebSocketsFixture>
{
    private readonly EventSubWebSocketsFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(EventSubTestTypes))]
    public async Task Send_WebhookEventSubCreateGetDeleteRequests_ReturnSuccessResponses(string subscriptionTypeName, EventSubTransportMethod transportMethod)
    {
        const string CALLBACK_URI = "https://fake-callback.xyz";

        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        EventSubSubscriptionTransportSpecification transport = transportMethod switch
        {
            _ when transportMethod == EventSubTransportMethod.Webhook
                => new WebhookSubscriptionTransport(new(CALLBACK_URI), "FAKE_SECRET"),
            _ when transportMethod == EventSubTransportMethod.Websocket
                => new WebsocketSubscriptionTransport(_fixture.SessionId),
            _ => throw new NotSupportedException() // We should eventually add conduit transport tests too.
        };

        CreateEventSubSubscriptionRequest createRequest = new()
        {
            Subscription = new()
            {
                Type = _fixture.GetSubscriptionType(subscriptionTypeName),
                Transport = new WebhookSubscriptionTransport(new(CALLBACK_URI), "FAKE_SECRET")
            }
        };

        var createResponse = await client.SendAsync(createRequest, ct);
        EventSubSubscription subscription = createResponse.Content.Data.Single();

        Assert.Equal(createRequest.Subscription.Type.Type.Type, (string)subscription.Type);
        Assert.Equal(createRequest.Subscription.Type.Type.Version, (string)subscription.Version);
        Assert.True(createRequest.Subscription.Type.Condition.All(kvp => subscription.Condition.GetValueOrDefault(kvp.Key) == (string)kvp.Value));
        Assert.Equal(createRequest.Subscription.Transport.Method, subscription.Transport.Method);
        if (subscription.Transport.Method == EventSubTransportMethod.Websocket)
        {
            Assert.NotNull(subscription.Transport.SessionId);
            Assert.Equal(createRequest.Subscription.Transport.SessionId!, subscription.Transport.SessionId);
        }
        if (subscription.Transport.Method == EventSubTransportMethod.Webhook)
        {
            Assert.NotNull(subscription.Transport.Callback);
            Assert.Equal(CALLBACK_URI, subscription.Transport.Callback.AbsoluteUri);
        }

        await Task.Delay(100, ct);

        GetEventSubSubscriptionsRequest getRequest = new()
        {
            SubscriptionId = subscription.Id
        };
        var getResponse = await client.SendAsync(getRequest, ct);
        Assert.NotEmpty(getResponse.Content.Data);

        DeleteEventSubSubscriptionRequest deleteRequest = new()
        {
            SubscriptionId = subscription.Id
        };
        await client.SendAsync(deleteRequest, ct);
    }
}
