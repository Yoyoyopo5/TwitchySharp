using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

public class Test_WebhookHashVerifier
{
    private readonly static EventSubSubscription FAKE_SUBSCRIPTION = new()
    {
        Id = new("f1c2a387-161a-49f9-a165-0f21d7a4e1c4"),
        Status = EventSubSubscriptionStatus.Enabled,
        Type = new("channel.follow"),
        Version = new("1"),
        Cost = 1,
        Condition = new Dictionary<string, object>() { { "broadcaster_user_id", "12826" } }.ToImmutableDictionary(),
        CreatedAt = DateTimeOffset.Parse("2019-11-16T10:11:12.634234626Z"),
        Transport = new() { Method = EventSubTransportMethod.Webhook, Callback = new("https://example.com/webhooks/callback") }
    };

    private const string FAKE_PAYLOAD_JSON = """
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

    private static EventSubWebhookRequestHeader CreateUnsignedHeader()
        => new()
        {
            TwitchEventsubMessageId = new("fake-id"),
            TwitchEventsubMessageTimestamp = new("2024-01-01T12:00:00Z"),
            TwitchEventsubMessageSignature = new(string.Empty), // we can calc this per test
            TwitchEventsubMessageType = new(string.Empty),
            TwitchEventsubSubscriptionType = new("channel.follow"),
            TwitchEventsubSubscriptionVersion = new("1")
        };

    private static ResolveWebhookSecret CreateFakeResolver(WebhookSecret secret)
        => (_, _) => ValueTask.FromResult<WebhookSecret?>(secret);

    private static VerifyWebhookHash CreateStubVerifier(WebhookSecret secret)
        => WebhookHashVerifier.Create(CreateFakeResolver(secret));

    [Fact]
    public async Task VerifyMessage_ValidMessageSignature_ReturnsValid()
    {
        const string FAKE_SECRET = "super_secure_secret";
        WebhookSecret secret = new(FAKE_SECRET);

        EventSubWebhookRequestHeader fakeHeader = CreateUnsignedHeader().Sign(FAKE_PAYLOAD_JSON, secret);
        using MemoryStream bodyStream = FAKE_PAYLOAD_JSON.ToMemoryStream();

        EventSubWebhookRequest fakeRequest = new()
        {
            Header = fakeHeader,
            Content = new(bodyStream)
        };

        VerifyWebhookHash stubVerifier = CreateStubVerifier(secret);
        Validation result = await stubVerifier(FAKE_SUBSCRIPTION, fakeRequest, TestContext.Current.CancellationToken);

        result.Match(
            onError: e => throw new Exception($"Verifier returned an error: {e.Message}."),
            onValid: () => true
            );
    }

    [Fact]
    public async Task VerifyMessage_InvalidMessageSignature_ReturnsError()
    {
        const string FAKE_SERVER_SECRET = "super_secure_secret";
        const string FAKE_CLIENT_SECRET = "wrong_secret";
        WebhookSecret serverSecret = new(FAKE_SERVER_SECRET);
        WebhookSecret clientSecret = new(FAKE_CLIENT_SECRET);

        EventSubWebhookRequestHeader fakeHeader = CreateUnsignedHeader().Sign(FAKE_PAYLOAD_JSON, serverSecret);
        using MemoryStream bodyStream = FAKE_PAYLOAD_JSON.ToMemoryStream();

        EventSubWebhookRequest fakeRequest = new()
        {
            Header = fakeHeader,
            Content = new(bodyStream)
        };

        VerifyWebhookHash stubVerifier = CreateStubVerifier(clientSecret);
        Validation result = await stubVerifier(FAKE_SUBSCRIPTION, fakeRequest, TestContext.Current.CancellationToken);

        result.Match(
            onError: e => true,
            onValid: () => throw new Exception("Verifier returned valid (expected Error).")
            );
    }
}

internal static class EventSubWebhookRequestExtensions
{
    public static EventSubWebhookRequestHeader Sign(this EventSubWebhookRequestHeader header, string body, WebhookSecret secret)
    {
        byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
        using HMACSHA256 hmac = new(secretBytes);
        byte[] fakeSignature = hmac.ComputeHash(Encoding.UTF8.GetBytes(header.TwitchEventsubMessageId + header.TwitchEventsubMessageTimestamp + body));
        string signature = "sha256=" + Convert.ToHexString(fakeSignature);
        return header with { TwitchEventsubMessageSignature = new(signature) };
    }
}
