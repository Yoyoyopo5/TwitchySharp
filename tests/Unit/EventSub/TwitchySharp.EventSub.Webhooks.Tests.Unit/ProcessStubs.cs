using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Serialization;
using TwitchySharp.EventSub.Webhooks.Functional;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

internal record FakeWebhookRequestContent(string Body) : WebhookRequestContent;

internal class ProcessStubs
{
    private const string FAKE_REQUEST_BODY = "request_data";

    public static EventSubWebhookRequest CreateFakeRequest(string? requestBody = null, EventSubWebhookRequestHeader? header = null)
        => new()
        {
            Header = header ?? new EventSubWebhookRequestHeader()
            {
                TwitchEventsubMessageId = new("12345"),
                TwitchEventsubSubscriptionType = new("fake-subscription"),
                TwitchEventsubSubscriptionVersion = new("1"),
                TwitchEventsubMessageType = EventSubWebhookMessageType.Notification,
                TwitchEventsubMessageTimestamp = new("12387447"),
                TwitchEventsubMessageSignature = new("very-legit-signature")
            },
            Content = new(new MemoryStream(Encoding.UTF8.GetBytes(requestBody ?? FAKE_REQUEST_BODY)))
        };

    private static FakeWebhookRequestContent CreateFakeContent(NotificationPayloadStream bodyStream)
    {
        using StreamReader sr = new(bodyStream);
        return new FakeWebhookRequestContent(sr.ReadToEnd())
        {
            Subscription = new EventSubSubscription()
            {
                Id = new("123"),
                Status = EventSubSubscriptionStatus.Enabled,
                CreatedAt = DateTimeOffset.MinValue,
                Cost = 1,
                Transport = new EventSubTransport()
                {
                    Method = EventSubTransportMethod.Webhook,
                    Callback = new("https://fake-callback.com")
                },
                Type = new("fake-subscription"),
                Version = new("1")
            }
        };
    }

    public static ProcessWebhookRequest StubProcess { get; }
        = (request, ct) => ValueTask.FromResult<Validation<WebhookRequestContent>>(CreateFakeContent(request.Content));
}
