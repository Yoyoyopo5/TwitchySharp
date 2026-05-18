using TwitchySharp.EventSub.Webhooks.Enums;

namespace TwitchySharp.EventSub.Webhooks.Http;

/// <summary>
/// Represents the HTTP header for an EventSub webhook request.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#list-of-request-headers">List of Request Headers</see> for more information.
/// </remarks>
public record EventSubWebhookRequestHeader
{
    /// <inheritdoc cref="EventSubMessageId"/>
    public required EventSubMessageId TwitchEventsubMessageId { get; init; }
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Twitch sends you a notification at least once. If Twitch is unsure of whether you received a notification, it’ll resend the event, which means you may receive a notification twice.
    /// If this is an issue for your implementation, see <see href="https://dev.twitch.tv/docs/eventsub#handling-duplicate-events">Handling duplicates</see> for options.
    /// </remarks>
    public string? TwitchEventsubMessageRetry { get; init; } // Unsure of how this header is actually used, not clear in documentation.
    /// <summary>
    /// The type of notification.
    /// </summary>
    public required EventSubWebhookMessageType TwitchEventsubMessageType { get; init; }
    /// <inheritdoc cref="EventSubMessageSignature"/>
    public required EventSubMessageSignature TwitchEventsubMessageSignature { get; init; }
    /// <inheritdoc cref="EventSubMessageTimestamp"/>
    public required EventSubMessageTimestamp TwitchEventsubMessageTimestamp { get; init; }
    /// <summary>
    /// The subscription type you subscribed to. For example, <c>channel.follow</c>.
    /// </summary>
    public required EventSubSubscriptionTypeName TwitchEventsubSubscriptionType { get; init; }
    /// <summary>
    /// The version number that identifies the definition of the subscription request.
    /// This version matches the version number that you specified in your subscription request.
    /// </summary>
    public required EventSubSubscriptionTypeVersion TwitchEventsubSubscriptionVersion { get; init; }
}
