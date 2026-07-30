using TwitchySharp.Api.Helix.EventSub;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public class Test_WebhookEventSubSubscriptions(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Theory]
    [ClassData(typeof(EventSubTestProvider))]
    public async Task WebhookEventSubCreateGetDelete(EventSubTestRow subscriptionTestData)
    {
        EventSubCallbackUrl callbackUrl = new("https://fake-callback.xyz");
        WebhookSecret secret = new("FAKE_SECRET");

        await _fixture.SendEventSubSubscriptionRequests(
            EventSubTestRegistry.Get(subscriptionTestData.SubscriptionType),
            new WebhookSubscriptionTransport(callbackUrl, secret),
            TestContext.Current.CancellationToken
            );
    }
}
