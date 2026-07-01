using Microsoft.AspNetCore.Http;

namespace TwitchySharp.EventSub.Webhooks.Tests.Integration;

public class Test_HashValidation(WebhooksFixture fixture) : IClassFixture<WebhooksFixture>
{
    private readonly WebhooksFixture _fixture = fixture;

    [Fact]
    public async Task Receive_NotificationWithInvalidSignature_IgnoreRequest()
    {
        const string FAKE_MESSAGE_ID = "1234567890";
        const string FAKE_BODY = $$"""
                        {
              "subscription": {
                "id": "{{FAKE_MESSAGE_ID}}",
                "status": "enabled",
                "type": "channel.follow",
                "version": "1",
                "cost": 1,
                "condition": {
                  "broadcaster_user_id": "12826"
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
        const string FAKE_INVALID_SECRET = "bad_secret";
        DateTimeOffset fakeTimestamp = new(2026, 6, 30, 19, 42, 0, TimeSpan.Zero);

        HttpContext request = await WebhookRequest.Create(
            new(FAKE_INVALID_SECRET),
            new(FAKE_MESSAGE_ID),
            Functional.EventSubWebhookMessageType.Notification,
            EventSubSubscriptionType.ChannelFollow,
            fakeTimestamp,
            FAKE_BODY
            );

        await _fixture.SimulateRequest(request, TestContext.Current.CancellationToken);

        Assert.Null(_fixture.Handler.LastNotification);
    }
}
