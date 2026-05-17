using TwitchySharp.EventSub.Webhooks.Http;

namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// An individual EventSub webhook request, including header and content.
/// </summary>
public record EventSubWebhookRequest
{
    public required EventSubWebhookRequestHeader Header { get; init; }
    public required NotificationPayloadStream Content { get; init; }
}
