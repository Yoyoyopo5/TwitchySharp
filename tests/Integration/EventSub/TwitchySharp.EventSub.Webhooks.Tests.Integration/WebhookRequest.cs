using System.Text;
using Microsoft.AspNetCore.Http;
using TwitchySharp.EventSub.Webhooks.Crypto;
using TwitchySharp.EventSub.Webhooks.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Integration;

public static class WebhookRequest
{
    public static async ValueTask<HttpContext> Create(WebhookSecret secret, WebhookMessageId messageId, EventSubWebhookMessageType messageType, EventSubSubscriptionType subscriptionType, DateTimeOffset timestamp, string body)
    {
        DefaultHttpContext context = new();
        context.Request.Headers.Append("Twitch-Eventsub-Message-Id", messageId.Value);
        context.Request.Headers.Append("Twitch-Eventsub-Message-Type", messageType.Value);
        context.Request.Headers.Append("Twitch-Eventsub-Subscription-Type", subscriptionType.Type.Value);
        context.Request.Headers.Append("Twitch-Eventsub-Subscription-Version", subscriptionType.Version.Value);
        context.Request.Headers.Append("Twitch-Eventsub-Message-Timestamp", timestamp.ToUnixTimeSeconds().ToString());
        context.Request.Headers.Append("Twitch-Eventsub-Message-Signature", await ComputeSignature(secret, messageId, timestamp, body));

        context.Request.Method = "POST";
        context.Request.Host = new("https://twitch.tv");
        context.Request.Path = new(WebhooksFixture.WEBHOOKS_PATH);
        context.Request.Body = body.ToStream();

        return context;
    }

    private static MemoryStream ToStream(this string data)
        => new(Encoding.UTF8.GetBytes(data));

    private static async ValueTask<string> ComputeSignature(WebhookSecret secret, WebhookMessageId messageId, DateTimeOffset timestamp, string body)
    {
        using Stream bodyStream = body.ToStream();
        byte[] signatureBytes = await EventSubWebhookCrypto.ComputeSignature(
            secretBytes: secret.ToBytes(),
            messageId,
            timestamp.ToUnixTimeSeconds().ToString(),
            bodyStream,
            TestContext.Current.CancellationToken
            );
        return Encoding.UTF8.GetString(signatureBytes);
    }
}
