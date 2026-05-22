using TwitchySharp.EventSub.Serialization;

namespace TwitchySharp.EventSub.Webhooks.Functional;

/// <summary>
/// An individual EventSub webhook request, including header and content.
/// </summary>
public record EventSubWebhookRequest
{
    /// <summary>
    /// The EventSub webhook request header.
    /// </summary>
    public required EventSubWebhookRequestHeader Header { get; init; }
    /// <summary>
    /// The EventSub webhook request body.
    /// </summary>
    public required NotificationPayloadStream Content { get; init; }
}
