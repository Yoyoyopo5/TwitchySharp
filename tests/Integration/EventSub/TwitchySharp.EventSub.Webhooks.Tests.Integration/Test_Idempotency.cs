using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TwitchySharp.EventSub.Webhooks.Tests.Integration;

public class Test_Idempotency(WebhooksFixture fixture) : IClassFixture<WebhooksFixture>
{
    private readonly WebhooksFixture _fixture = fixture;

    [Fact]
    public async Task Receive_TwoRequestsWithSameId_IgnoreSecond()
    {
        const string FAKE_MESSAGE_ID = "1234567890";
        DateTimeOffset fakeTimestamp = new(2026, 6, 30, 19, 42, 0, TimeSpan.Zero);

        string CreateBody(int cost)
            => $$"""
            {
              "subscription": {
                "id": "{{FAKE_MESSAGE_ID}}",
                "status": "enabled",
                "type": "channel.follow",
                "version": "2",
                "cost": {{cost}},
                "condition": {
                  "broadcaster_user_id": "12826",
                  "moderator_user_id": "12382"
                },
                "transport": {
                  "method": "webhook",
                  "callback": "https://example.com/webhooks/callback"
                },
                "created_at": "2019-11-16T10:11:12.634234626Z"
              },
              "event": {
                "user_id": "1337",
                "user_login": "awesome_user",
                "user_name": "Awesome_User",
                "broadcaster_user_id":     "12826",
                "broadcaster_user_login":  "twitch",
                "broadcaster_user_name":   "Twitch",
                "followed_at": "2020-07-15T18:16:11.17106713Z"
              }
            }
            """;

        HttpContext request = await WebhookRequest.Create(
            _fixture.WebhooksSecret,
            new(FAKE_MESSAGE_ID),
            Functional.EventSubWebhookMessageType.Notification,
            EventSubSubscriptionType.ChannelFollow,
            fakeTimestamp,
            CreateBody(1)
            );

        HttpContext repeatedRequest = await WebhookRequest.Create(
            _fixture.WebhooksSecret,
            new(FAKE_MESSAGE_ID),
            Functional.EventSubWebhookMessageType.Notification,
            EventSubSubscriptionType.ChannelFollow,
            fakeTimestamp,
            CreateBody(2)
            );

        await _fixture.SimulateRequest(request, TestContext.Current.CancellationToken);
        await _fixture.SimulateRequest(repeatedRequest, TestContext.Current.CancellationToken);

        Assert.Equal(1, _fixture.Handler.LastNotification?.Subscription.Cost);
    }
}
