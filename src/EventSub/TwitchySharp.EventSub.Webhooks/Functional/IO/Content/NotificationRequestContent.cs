using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Webhooks.Functional;

// This is slightly weird because the notification payload is actually the Notification itself,
// but we wrap it into a WebhookRequestContent so we can handle every request polymorphically.
// When we pass the notification to the handler, we unwrap the notification.
// This means that this object is not a 1:1 representation of the incoming request content like
// the other request types.
/// <summary>
/// The content of an EventSub webhook notification request.
/// </summary>
public record NotificationRequestContent : WebhookRequestContent
{
    /// <summary>
    /// The notification data.
    /// </summary>
    public required IEventSubNotification Notification { get; init; }
}
