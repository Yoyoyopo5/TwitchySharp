using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;
using TwitchySharp.Api.Tests.Integration.Fixtures;

namespace TwitchySharp.Api.Tests.Integration.Tests;

public class Test_EventSubSubscriptions(TwitchApiTestFixture fixture) : IClassFixture<TwitchApiTestFixture>
{
    private readonly TwitchApiTestFixture _fixture = fixture;

    [Fact]
    public async Task CreateEventSubSubscription_RequestSucceeds()
    {
        ITwitchClient client = _fixture.CreateTwitchClient();
        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new()
            {
                Transport = new WebhookSubscriptionTransport(new("https://fake-callback.com"), new("super_secure_secret")),
                Type = new StreamOnline(TwitchApiTestFixture.TestUserId)
            }
        };

        await client.SendAsync(request, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GetEventSubSubscriptions_ThenPageRequest_BothRequestsSucceed()
    {
        ITwitchClient client = _fixture.CreateTwitchClient();
        GetEventSubSubscriptionsRequest request = new();

        TwitchResponse<GetEventSubSubscriptionsResponse> response
            = await client.SendAsync(request, TestContext.Current.CancellationToken);

        GetEventSubSubscriptionsRequest pagedRequest = request with { After = response.Content.Pagination.Cursor };

        await client.SendAsync(pagedRequest, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task DeleteEventSubSubscription_FromSubscription_RequestSucceeds()
    {
        ITwitchClient client = _fixture.CreateTwitchClient();

        GetEventSubSubscriptionsRequest getRequest = new();
        TwitchResponse<GetEventSubSubscriptionsResponse> getResponse
            = await client.SendAsync(getRequest, TestContext.Current.CancellationToken);

        DeleteEventSubSubscriptionRequest deleteRequest = new(getResponse.Content.Data.First());

        await client.SendAsync(deleteRequest, TestContext.Current.CancellationToken);

    }
}
