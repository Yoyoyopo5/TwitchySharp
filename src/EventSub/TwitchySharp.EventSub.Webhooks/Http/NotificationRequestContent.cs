using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Webhooks.Http;

// This is slightly weird because the notification payload is actually the Notification itself,
// but we wrap it into a WebhookRequestContent so we can handle every request polymorphically.
// When we pass the notification to the handler, we unwrap the notification.
// This means that this object is not a 1:1 representation of the incoming request content like
// the other request types.
internal record NotificationRequestContent : WebhookRequestContent
{
    public required IEventSubNotification Notification { get; init; }
}
