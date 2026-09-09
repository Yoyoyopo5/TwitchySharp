using System.Collections.Immutable;
using Microsoft.AspNetCore.Http;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Subscriptions;

namespace TwitchySharp.Api.Tests.Integration.Helix;

public class Test_CreateEventSubSubscription(TwitchApiIntegrationTestFixture fixture)
{
    [Fact]
    public async Task SendAsync_CreateEventSubSubscriptionRequest_ResponseContainsExpectedData()
    {
        EventSubSubscription expectedSubscription = new()
        {
            Id = new("1238774239"),
            Status = EventSubSubscriptionStatus.Enabled,
            Type = EventSubSubscriptionTypeName.ChannelBan,
            Version = EventSubSubscriptionTypeVersion.Version1,
            Transport = new EventSubSubscriptionTransport()
            {
                Method = EventSubTransportMethod.Websocket,
                SessionId = new("12491724"),
                ConnectedAt = DateTime.MinValue
            },
            CreatedAt = DateTime.MinValue,
            Cost = 1,
            Condition = new Dictionary<ConditionKey, string>()
                {
                    { new ConditionKey("broadcaster_user_id"), "12374" }
                }.ToImmutableDictionary()
        };
        CreateEventSubSubscriptionResponseContent expectedResponseContent = new()
        {
            MaxTotalCost = 100,
            Total = 1,
            TotalCost = 1,
            Data = [expectedSubscription]
        };

        using IDisposable endpoint = fixture.TestServer.Map(HttpMethod.Post, "/helix/eventsub/subscriptions",
            (CreateEventSubSubscriptionRequestData data) => Results.Accepted(value: expectedResponseContent));

        CreateEventSubSubscriptionRequest request = new()
        {
            Subscription = new()
            {
                Transport = new WebhookSubscriptionTransport(new("https://fake-callback.com"), new("super_secure_secret")),
                Type = new ChannelBan(new("12374"))
            }
        };

        TwitchResponse<CreateEventSubSubscriptionResponseContent> response = await fixture.TestServer.GetDefaultTwitchClient().SendAsync(request, TestContext.Current.CancellationToken);

        Assert.Equal(expectedResponseContent.MaxTotalCost, response.Content.MaxTotalCost);
        Assert.Equal(expectedResponseContent.Total, response.Content.Total);
        Assert.Equal(expectedResponseContent.TotalCost, response.Content.TotalCost);
        EventSubSubscription subscription = Assert.Single(response.Content.Data);
        Assert.Equal(expectedSubscription.Id, subscription.Id);
        Assert.Equal(expectedSubscription.Type, subscription.Type);
        Assert.Equal(expectedSubscription.Version, subscription.Version);
        Assert.Equal(expectedSubscription.Status, subscription.Status);
        Assert.Equal(expectedSubscription.Transport, subscription.Transport);
        Assert.Equal(expectedSubscription.Cost, subscription.Cost);
        Assert.Equal((IEnumerable<KeyValuePair<ConditionKey, string>>)expectedSubscription.Condition, subscription.Condition);
    }
}
