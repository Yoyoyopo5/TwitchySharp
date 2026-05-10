namespace TwitchySharp.EventSub.Webhooks;

public record EventSubWebhookRequest
{
    public required EventSubWebhookRequestHeader Header { get; init; }
    public required NotificationPayloadStream Content { get; init; }
}
