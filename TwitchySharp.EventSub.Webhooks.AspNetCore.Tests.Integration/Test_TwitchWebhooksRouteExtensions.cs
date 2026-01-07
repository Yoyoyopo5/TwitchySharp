using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Notifications.Channel;
using TwitchySharp.EventSub.Webhooks.SignatureComputers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore.Tests.Integration;

public class Test_TwitchWebhooksRouteExtensions(WebhooksFixture fixture)
    : IClassFixture<WebhooksFixture>
{
    private readonly WebhooksFixture _fixture = fixture;

    [Fact]
    public async Task Respond_ValidCallbackRequest_ValidResponse()
    {
        const string FAKE_SUBSCRIPTION_ID = "f1c2a387-161a-49f9-a165-0f21d7a4e1c4";
        const string FAKE_CHALLENGE = "test_challenge";
        const string FAKE_MESSAGE_ID = "1234567890";
        const string FAKE_TIMESTAMP = "2019-11-16T10:11:12.634234626Z";
        const string FAKE_BODY = $$"""
            {
              "challenge": "{{FAKE_CHALLENGE}}",
              "subscription": {
                "id": "{{FAKE_SUBSCRIPTION_ID}}",
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

        DefaultTwitchWebhookCrypto stubCrypto = new();
        string fakeSignature = Encoding.UTF8.GetString(await stubCrypto.ComputeSignature(Encoding.UTF8.GetBytes(_fixture.Secret), FAKE_MESSAGE_ID, FAKE_TIMESTAMP, FAKE_BODY));

        IHeaderDictionary fakeHeaders = new EventSubWebhookRequestHeader()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageType = "webhook_callback_verification",
            TwitchEventsubMessageSignature = fakeSignature,
            TwitchEventsubMessageTimestamp = FAKE_TIMESTAMP,
            TwitchEventsubSubscriptionType = "channel.follow",
            TwitchEventsubSubscriptionVersion = "1"
        }.ToHeaderDictionary();

        HttpRequestMessage fakeCallbackRequest = new HttpRequestMessage(HttpMethod.Post, _fixture.Path)
        {
            Content = new StringContent(FAKE_BODY)
        }.AddHeaders(fakeHeaders);

        HttpClient stubClient = _fixture.CreateClient();
        HttpResponseMessage actualReponse = await stubClient.SendAsync(fakeCallbackRequest);
        EventSubSubscription? actualSubscription = _fixture.Handler.ActiveSubscription;

        HttpStatusCode actualResponseStatusCode = actualReponse.StatusCode;
        string actualResponseBody = await actualReponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, actualResponseStatusCode);
        Assert.Equal(FAKE_CHALLENGE, actualResponseBody);
        Assert.NotNull(actualSubscription);
        Assert.Equal(FAKE_SUBSCRIPTION_ID, actualSubscription.Id);
    }

    [Fact]
    public async Task Respond_ValidChannelChatMessageNotification_200Response()
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

        DefaultTwitchWebhookCrypto stubCrypto = new();
        string fakeSignature = Encoding.UTF8.GetString(await stubCrypto.ComputeSignature(Encoding.UTF8.GetBytes(_fixture.Secret), FAKE_MESSAGE_ID, FAKE_TIMESTAMP, FAKE_BODY));

        IHeaderDictionary fakeHeaders = new EventSubWebhookRequestHeader()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageType = "notification",
            TwitchEventsubMessageSignature = fakeSignature,
            TwitchEventsubMessageTimestamp = FAKE_TIMESTAMP,
            TwitchEventsubSubscriptionType = "channel.chat.message",
            TwitchEventsubSubscriptionVersion = "1"
        }.ToHeaderDictionary();

        HttpRequestMessage fakeNotificationRequest = new HttpRequestMessage(HttpMethod.Post, _fixture.Path)
        {
            Content = new StringContent(FAKE_BODY)
        }.AddHeaders(fakeHeaders);

        HttpClient stubClient = _fixture.CreateClient();
        HttpResponseMessage actualReponse = await stubClient.SendAsync(fakeNotificationRequest);
        ChannelChatMessageNotification? actualNotification = _fixture.Handler.LastNotification as ChannelChatMessageNotification;

        HttpStatusCode actualResponseStatusCode = actualReponse.StatusCode;

        Assert.Equal(HttpStatusCode.OK, actualResponseStatusCode);
        Assert.NotNull(actualNotification);
        Assert.Equal(FAKE_MESSAGE, actualNotification.Event.Message.Text);
    }

    [Fact]
    public async Task Respond_ValidRevocationRequest_204Response()
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

        DefaultTwitchWebhookCrypto stubCrypto = new();
        string fakeSignature = Encoding.UTF8.GetString(await stubCrypto.ComputeSignature(Encoding.UTF8.GetBytes(_fixture.Secret), FAKE_MESSAGE_ID, FAKE_TIMESTAMP, FAKE_BODY));

        IHeaderDictionary fakeHeaders = new EventSubWebhookRequestHeader()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageType = "revocation",
            TwitchEventsubMessageSignature = fakeSignature,
            TwitchEventsubMessageTimestamp = FAKE_TIMESTAMP,
            TwitchEventsubSubscriptionType = "channel.follow",
            TwitchEventsubSubscriptionVersion = "1"
        }.ToHeaderDictionary();

        HttpRequestMessage fakeRevocationRequest = new HttpRequestMessage(HttpMethod.Post, _fixture.Path)
        {
            Content = new StringContent(FAKE_BODY)
        }.AddHeaders(fakeHeaders);

        EventSubSubscription fakePreexistingSubscription = new()
        {
            Id = FAKE_SUBSCRIPTION_ID,
            Cost = 1,
            CreatedAt = DateTimeOffset.UtcNow,
            Status = EventSubSubscriptionStatus.Enabled,
            Transport = new()
            {
                Method = EventSubTransportMethod.Webhook,
                Callback = "https://example.com/webhooks/callback"
            },
            Type = "channel.follow",
            Version = "1"
        };
        _fixture.Handler.ActiveSubscription = fakePreexistingSubscription;

        HttpClient stubClient = _fixture.CreateClient();
        HttpResponseMessage actualReponse = await stubClient.SendAsync(fakeRevocationRequest);
        EventSubSubscription? actualSubscription = _fixture.Handler.ActiveSubscription;

        HttpStatusCode actualResponseStatusCode = actualReponse.StatusCode;

        Assert.Equal(HttpStatusCode.NoContent, actualResponseStatusCode);
        Assert.Null(actualSubscription);
    }

    [Fact]
    public async Task Respond_InvalidHeadersRequest_400Response()
    {
        HeaderDictionary fakeInvalidHeaders = new()
        {
            ["Invalid-Message-Id"] = "12345"
        };

        HttpRequestMessage fakeInvalidRequest = new HttpRequestMessage(HttpMethod.Post, _fixture.Path)
        {
            Content = new StringContent("{}")
        }.AddHeaders(fakeInvalidHeaders);

        HttpClient stubClient = _fixture.CreateClient();
        HttpResponseMessage actualReponse = await stubClient.SendAsync(fakeInvalidRequest);

        Assert.Equal(HttpStatusCode.BadRequest, actualReponse.StatusCode);
    }

    [Fact]
    public async Task Respond_InvalidSecretRequest_401Response()
    {
        const string FAKE_MESSAGE_ID = "1234567890";
        const string FAKE_TIMESTAMP = "2023-11-06T18:11:47.492253549Z";
        const string FAKE_BODY = "{}";
        const string FAKE_INVALID_SECRET = "bad_secret";

        DefaultTwitchWebhookCrypto stubCrypto = new();
        string fakeSignature = Encoding.UTF8.GetString(await stubCrypto.ComputeSignature(Encoding.UTF8.GetBytes(FAKE_INVALID_SECRET), FAKE_MESSAGE_ID, FAKE_TIMESTAMP, FAKE_BODY));

        IHeaderDictionary fakeHeaders = new EventSubWebhookRequestHeader()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageType = "notification",
            TwitchEventsubMessageSignature = fakeSignature,
            TwitchEventsubMessageTimestamp = FAKE_TIMESTAMP,
            TwitchEventsubSubscriptionType = "channel.follow",
            TwitchEventsubSubscriptionVersion = "1"
        }.ToHeaderDictionary();

        HttpRequestMessage fakeInvalidSecretRequest = new HttpRequestMessage(HttpMethod.Post, _fixture.Path)
        {
            Content = new StringContent(FAKE_BODY)
        }.AddHeaders(fakeHeaders);

        _fixture.Handler.LastNotification = null; // leaky handler.

        HttpClient stubClient = _fixture.CreateClient();
        HttpResponseMessage actualReponse = await stubClient.SendAsync(fakeInvalidSecretRequest);

        HttpStatusCode actualResponseStatusCode = actualReponse.StatusCode;
        IEventSubNotification? actualNotification = _fixture.Handler.LastNotification;

        Assert.Equal(HttpStatusCode.Unauthorized, actualResponseStatusCode);
        Assert.Null(actualNotification);
    }
}