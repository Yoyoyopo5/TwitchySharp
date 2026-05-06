using TwitchySharp.EventSub.Models.Notifications;

namespace TwitchySharp.EventSub.Webhooks.Requests;

internal record NotificationRequestData : WebhookRequestData
{
    public required IEventSubNotification Notification { get; init; }
}
