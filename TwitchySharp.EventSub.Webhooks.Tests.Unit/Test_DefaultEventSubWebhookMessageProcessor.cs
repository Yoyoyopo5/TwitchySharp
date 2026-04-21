using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Webhooks.Responses;
using TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

public class Test_DefaultEventSubWebhookMessageProcessor
{
    private class StubWebhookHandler : IWebhookEventSubHandler
    {
        public ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default)
            => ValueTask.CompletedTask;

        public ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct)
            => ValueTask.CompletedTask;

        public ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct)
            => ValueTask.CompletedTask;
    }

    [Fact]
    public async Task HandleMessage_ValidNotificationMessage_ReturnOkResponse()
    {
        const string FAKE_SECRET = "super_secure_secret";
        byte[] fakeSecretBytes = Encoding.UTF8.GetBytes(FAKE_SECRET);
        const string FAKE_MESSAGE_ID = "12345";
        const string FAKE_MESSAGE_TIMESTAMP = "2024-06-01T12:00:00Z";
        const string FAKE_MESSAGE_TYPE = "notification";
        string fakeSubscriptionType = EventSubSubscriptionType.ChannelChatMessage.Type;
        string fakeSubscriptionVersion = EventSubSubscriptionType.ChannelChatMessage.Version;
        string fakeBody = """
            {
              "subscription": {
                "id": "0b7f3361-672b-4d39-b307-dd5b576c9b27",
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
                "created_at": "2023-11-06T18:11:47.492253549Z",
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
                  "text": "Hi chat",
                  "fragments": [
                    {
                      "type": "text",
                      "text": "Hi chat",
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
        MemoryStream bodyStream = new(Encoding.UTF8.GetBytes(fakeBody));

        using HMACSHA256 hmac = new(fakeSecretBytes);
        string fakeSignature = "sha256=" + Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(FAKE_MESSAGE_ID + FAKE_MESSAGE_TIMESTAMP + fakeBody)));
        EventSubWebhookRequestHeader fakeHeader = new()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageTimestamp = FAKE_MESSAGE_TIMESTAMP,
            TwitchEventsubMessageType = new(FAKE_MESSAGE_TYPE),
            TwitchEventsubMessageSignature = fakeSignature,
            TwitchEventsubSubscriptionType = fakeSubscriptionType,
            TwitchEventsubSubscriptionVersion = fakeSubscriptionVersion
        };

        DefaultEventSubWebhookMessageProcessor processor = new(new StubWebhookHandler());

        WebhookResponseData actualResponse = await processor.HandleRequest(fakeHeader, bodyStream);

        Assert.Equal(200, actualResponse.StatusCode);
    }
}
