using Microsoft.AspNetCore.Http;
using TwitchySharp.EventSub.Webhooks.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Integration;

public class Test_Handler(WebhooksFixture fixture) : IClassFixture<WebhooksFixture>
{
    private readonly WebhooksFixture _fixture = fixture;

    [Fact]
    public async Task Receive_CallbackVerification_HandlerOnCallbackCalled()
    {
        const string FAKE_CHALLENGE = "test_challenge";
        const string FAKE_MESSAGE_ID = "1234567890";
        const string FAKE_BODY = $$"""
            {
              "challenge": "{{FAKE_CHALLENGE}}",
              "subscription": {
                "id": "f1c2a387-161a-49f9-a165-0f21d7a4e1c4",
                "status": "webhook_callback_verification_pending",
                "type": "channel.follow",
                "version": "1",
                "cost": 1,
                "condition": {
                  "broadcaster_user_id": "12826"
                },
                "transport": {
                  "method": "webhook",
                  "callback": "http://localhost/test-webhook"
                },
                "created_at": "2019-11-16T10:11:12.634234626Z"
              }
            }
            """;
        DateTimeOffset fakeTimestamp = new(2026, 6, 30, 13, 46, 0, TimeSpan.Zero);

        HttpContext request = await WebhookRequest.Create(
            _fixture.WebhooksSecret,
            new(FAKE_MESSAGE_ID),
            EventSubWebhookMessageType.WebhookCallbackVerification,
            EventSubSubscriptionType.ChannelFollow,
            fakeTimestamp,
            FAKE_BODY
            );

        await _fixture.SimulateRequest(request, TestContext.Current.CancellationToken);

        Assert.NotNull(_fixture.Handler.LastCallback);
        Assert.Equal(FAKE_CHALLENGE, _fixture.Handler.LastCallbackChallenge);
    }

    [Fact]
    public async Task Receive_Notification_HandlerOnNotifiedCalled()
    {
        const string FAKE_SUBSCRIPTION_ID = "0b7f3361-672b-4d39-b307-dd5b576c9b27";
        const string FAKE_MESSAGE_ID = "1234567890";
        const string FAKE_TIMESTAMP = "2023-11-06T18:11:47.492253549Z";
        const string FAKE_MESSAGE = "Whaddup chat? Check out Yoyoyopo5 on Twitch, it's good.";
        const string FAKE_BODY = $$"""
            {
              "subscription": {
                "id": "{{FAKE_SUBSCRIPTION_ID}}",
                "status": "enabled",
                "type": "channel.chat.message",
                "version": "1",
                "condition": {
                  "broadcaster_user_id": "1971641",
                  "user_id": "2914196"
                },
                "transport": {
                  "method": "websocket",
                  "session_id": "AgoQHR3s6Mb4T8GFB1l3DlPfiRIGY2VsbC1h"
                },
                "created_at": "{{FAKE_TIMESTAMP}}",
                "cost": 0
              },
              "event": {
                "broadcaster_user_id": "1971641",
                "broadcaster_user_login": "streamer",
                "broadcaster_user_name": "streamer",
                "chatter_user_id": "4145994",
                "chatter_user_login": "viewer32",
                "chatter_user_name": "viewer32",
                "message_id": "cc106a89-1814-919d-454c-f4f2f970aae7",
                "message": {
                  "text": "{{FAKE_MESSAGE}}",
                  "fragments": [
                    {
                      "type": "text",
                      "text": "{{FAKE_MESSAGE}}",
                      "cheermote": null,
                      "emote": null,
                      "mention": null
                    }
                  ]
                },
                "color": "#00FF7F",
                "badges": [
                  {
                    "set_id": "moderator",
                    "id": "1",
                    "info": ""
                  },
                  {
                    "set_id": "subscriber",
                    "id": "12",
                    "info": "16"
                  },
                  {
                    "set_id": "sub-gifter",
                    "id": "1",
                    "info": ""
                  }
                ],
                "message_type": "text",
                "cheer": null,
                "reply": null,
                "channel_points_custom_reward_id": null,
                "source_broadcaster_user_id": null,
                "source_broadcaster_user_login": null,
                "source_broadcaster_user_name": null,
                "source_message_id": null,
                "source_badges": null
              }
            }
            """;
        DateTimeOffset fakeTimestamp = new(2026, 6, 30, 19, 30, 0, TimeSpan.Zero);

        HttpContext request = await WebhookRequest.Create(
            _fixture.WebhooksSecret,
            new(FAKE_MESSAGE_ID),
            EventSubWebhookMessageType.Notification,
            EventSubSubscriptionType.ChannelChatMessage,
            fakeTimestamp,
            FAKE_BODY
            );

        await _fixture.SimulateRequest(request, TestContext.Current.CancellationToken);

        Assert.NotNull(_fixture.Handler.LastNotification);
    }

    [Fact]
    public async Task Receive_Revocation_HandlerOnRevokedCalled()
    {
        const string FAKE_SUBSCRIPTION_ID = "f1c2a387-161a-49f9-a165-0f21d7a4e1c4";
        const string FAKE_MESSAGE_ID = "1234567890";
        const string FAKE_TIMESTAMP = "2019-11-16T10:11:12.634234626Z";
        const string FAKE_BODY = $$"""
            {
              "subscription": {
                "id": "{{FAKE_SUBSCRIPTION_ID}}",
                "status": "authorization_revoked",
                "type": "channel.follow",
                "cost": 1,
                "version": "1",
                "condition": {
                  "broadcaster_user_id": "12826"
                },
                "transport": {
                  "method": "webhook",
                  "callback": "https://example.com/webhooks/callback"
                },
                "created_at": "{{FAKE_TIMESTAMP}}"
              }
            }
            """;
        DateTimeOffset fakeTimestamp = new(2026, 6, 30, 19, 37, 0, TimeSpan.Zero);

        HttpContext request = await WebhookRequest.Create(
            _fixture.WebhooksSecret,
            new(FAKE_MESSAGE_ID),
            EventSubWebhookMessageType.Revocation,
            EventSubSubscriptionType.ChannelFollow,
            fakeTimestamp,
            FAKE_BODY
            );

        await _fixture.SimulateRequest(request, TestContext.Current.CancellationToken);

        Assert.NotNull(_fixture.Handler.LastRevoked);
    }
}
