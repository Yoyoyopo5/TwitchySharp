using System.Collections.Immutable;
using TwitchySharp.EventSub.Notifications;
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
            Content = new((requestBody ?? FAKE_REQUEST_BODY).ToMemoryStream())
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

    public static EventSubSubscription FakeSubscription { get; } = new()
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
}

public record StubEventSubNotification : IEventSubNotification
{
    public EventSubSubscription Subscription => ProcessStubs.FakeSubscription;
}
