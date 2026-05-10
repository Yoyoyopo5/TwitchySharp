using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Webhooks.Enums;

/// <summary>
/// Contains static definitions for possible Twitch EventSub webhook message types.
/// </summary>
/// <remarks>
/// See more about EventSub webhook request header types at <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#list-of-request-headers">Handling Webhook Events</see>.
/// </remarks>
/// <param name="Value">The string value of the message type.</param>
[Wrapper<string>]
public readonly partial record struct EventSubWebhookMessageType(string Value)
{
    /// <summary>
    /// This type of webhook contains a specific event's data.
    /// </summary>
    public static EventSubWebhookMessageType Notification { get; } = new(EventSubWebhookMessageTypes.NOTIFICATION);
    /// <summary>
    /// This type of webhook contains the challenge used to verify that you own the event handler.
    /// </summary>
    public static EventSubWebhookMessageType WebhookCallbackVerification { get; } = new(EventSubWebhookMessageTypes.WEBHOOK_CALLBACK_VERIFICATION);
    /// <summary>
    /// This type of webhook contains the reason why Twitch revoked your subscription.
    /// </summary>
    public static EventSubWebhookMessageType Revocation { get; } = new(EventSubWebhookMessageTypes.REVOCATION);
}
