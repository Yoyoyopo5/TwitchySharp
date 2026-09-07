using System.Diagnostics.CodeAnalysis;
using TwitchySharp.Api.Authentication;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.EventSub;

public static class EventSubFixtureExtensions
{
    public async static Task SendEventSubSubscriptionRequests(
        this TwitchClientFixture fixture,
        EventSubTest testData,
        EventSubSubscriptionTransportSpecification transport,
        CancellationToken ct
        )
    {
        TestingTwitchClient client = fixture.GetTwitchApiClient();

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

    private static Task<TwitchResponse<DeleteEventSubSubscriptionResponseContent>> DeleteSubscription(
        this TestingTwitchClient client,
        EventSubSubscription subscription,
        CancellationToken ct
        )
        => client.SendAsync(new DeleteEventSubSubscriptionRequest(subscription), subscription.GetSubscriptionType().ToTestName(), ct);

    private async static Task<EventSubSubscription> GetSubscription(
        this TestingTwitchClient client,
        EventSubSubscription subscription,
        CancellationToken ct
        )
    {
        GetEventSubSubscriptionsRequest requestForSubscription = new() { SubscriptionId = subscription.Id };

        return (await client.SendAsync(
            requestForSubscription with
            {
                AuthenticationContext = subscription.ToSubscriptionTypeSpecification().Match(
                    e => throw new InvalidOperationException(e.Message),
                    spec => spec.AuthenticationContext.ToRequestAuthenticationContext(subscription.Transport.Method)
                    )
            },
            subscription.GetSubscriptionType().ToTestName(),
            ct
            )).Content.Data.Single();
    }

    private async static Task<EventSubSubscription> CreateSubscription(
        this TestingTwitchClient client,
        EventSubSubscriptionSpecification specification,
        CancellationToken ct
        )
        => (await client.SendAsync(new CreateEventSubSubscriptionRequest()
        {
            Subscription = specification
        }, specification.Type.Type.ToTestName(), ct)).Content.Data.Single();

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
