using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Webhooks.Http;

internal record NotificationRequestContent : WebhookRequestContent
{
    public required IEventSubNotification Notification { get; init; }
}
