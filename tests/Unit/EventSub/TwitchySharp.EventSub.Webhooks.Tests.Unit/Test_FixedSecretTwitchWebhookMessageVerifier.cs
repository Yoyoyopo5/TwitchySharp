using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Webhooks.MessageVerifiers;
using TwitchySharp.EventSub.Webhooks.SecretResolvers;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

public class Test_FixedSecretTwitchWebhookMessageVerifier
{
    [Fact]
    public async Task VerifyMessage_ValidMessage_ReturnsTrue()
    {
        const string FAKE_SECRET = "super_secure_secret";
        byte[] secretBytes = Encoding.UTF8.GetBytes(FAKE_SECRET);

        const string FAKE_MESSAGE_ID = "12345";
        const string FAKE_MESSAGE_TIMESTAMP = "2024-01-01T12:00:00Z";
        using HMACSHA256 hmac = new(secretBytes);
        
        string fakeNotificationData = """
            {
              "subscription": {
                "id": "f1c2a387-161a-49f9-a165-0f21d7a4e1c4",
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
        byte[] fakeSignature = hmac.ComputeHash(Encoding.UTF8.GetBytes(FAKE_MESSAGE_ID + FAKE_MESSAGE_TIMESTAMP + fakeNotificationData));
        EventSubWebhookRequestHeader fakeRequestHeaders = new()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageTimestamp = FAKE_MESSAGE_TIMESTAMP,
            TwitchEventsubMessageSignature = "sha256=" + Convert.ToHexString(fakeSignature),
            TwitchEventsubMessageType = new(string.Empty),
            TwitchEventsubSubscriptionType = string.Empty,
            TwitchEventsubSubscriptionVersion = string.Empty
        };

        DefaultTwitchWebhookMessageVerifier stubVerifier = new(new FixedSecretTwitchWebhookSecretsResolver(FAKE_SECRET));
        bool actualResult = await stubVerifier.IsValid(fakeRequestHeaders, fakeNotificationData);

        Assert.True(actualResult);
    }

    [Fact]
    public async Task VerifyMessage_InvalidMessage_ReturnsFalse()
    {
        const string FAKE_SECRET = "super_secure_secret";
        byte[] secretBytes = Encoding.UTF8.GetBytes(FAKE_SECRET);

        const string FAKE_MESSAGE_ID = "12345";
        const string FAKE_MESSAGE_TIMESTAMP = "2024-01-01T12:00:00Z";
        const string FAKE_INVALID_SIGNATURE = "sha256=INVALIDSIGNATURE";

        string fakeNotificationData = """
            {
              "subscription": {
                "id": "f1c2a387-161a-49f9-a165-0f21d7a4e1c4",
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
        EventSubWebhookRequestHeader fakeRequestHeaders = new()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageTimestamp = FAKE_MESSAGE_TIMESTAMP,
            TwitchEventsubMessageSignature = FAKE_INVALID_SIGNATURE,
            TwitchEventsubMessageType = new(string.Empty),
            TwitchEventsubSubscriptionType = string.Empty,
            TwitchEventsubSubscriptionVersion = string.Empty
        };

        DefaultTwitchWebhookMessageVerifier stubVerifier = new(new FixedSecretTwitchWebhookSecretsResolver(FAKE_SECRET));
        bool actualResult = await stubVerifier.IsValid(fakeRequestHeaders, fakeNotificationData);

        Assert.False(actualResult);
    }
}
