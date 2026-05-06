using System.Security.Cryptography;
using System.Text;
using TwitchySharp.EventSub.Webhooks.SignatureComputers;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

public class Test_DefaultTwitchWebhookCrypto
{
    [Fact]
    public async Task ComputeHash_ValidMessage_ReturnsExpectedHash()
    {
        const string FAKE_SECRET = "super_secure_secret";
        byte[] fakeSecretBytes = Encoding.UTF8.GetBytes(FAKE_SECRET);

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

        const string FAKE_MESSAGE_ID = "12345";
        const string FAKE_MESSAGE_TIMESTAMP = "2024-06-01T12:00:00Z";

        using HMACSHA256 hmac = new(fakeSecretBytes);
        string fakeSignature = "sha256=" + Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(FAKE_MESSAGE_ID + FAKE_MESSAGE_TIMESTAMP + fakeNotificationData)));
        EventSubWebhookRequestHeader fakeHeader = new()
        {
            TwitchEventsubMessageId = FAKE_MESSAGE_ID,
            TwitchEventsubMessageType = new(string.Empty),
            TwitchEventsubMessageSignature = fakeSignature,
            TwitchEventsubMessageTimestamp = FAKE_MESSAGE_TIMESTAMP,
            TwitchEventsubSubscriptionType = string.Empty,
            TwitchEventsubSubscriptionVersion = string.Empty
        };

        DefaultTwitchWebhookCrypto stubVerifier = new();
        byte[] actualHash = await stubVerifier.ComputeSignature(fakeSecretBytes, fakeHeader.TwitchEventsubMessageId, fakeHeader.TwitchEventsubMessageTimestamp, fakeNotificationData);

        string expectedHex = fakeSignature;
        string actualHex = Encoding.UTF8.GetString(actualHash);

        Assert.Equal(expectedHex, actualHex);
    }
}
