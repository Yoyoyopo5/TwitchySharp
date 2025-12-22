namespace TwitchySharp.EventSub.Webhooks;

/// <summary>
/// Represents the type of EventSub message received via webhook.
/// <br/>
/// See more about EventSub webhook request header types at <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#list-of-request-headers">Handling Webhook Events</see>.
/// </summary>
internal static class TwitchEventSubMessageTypes
{
    /// <summary>
    /// This type of webhook contains a specific event's data.
    /// </summary>
    public const string NOTIFICATION = "notification";
    /// <summary>
    /// This type of webhook contains the challenge used to verify that you own the event handler.
    /// </summary>
    public const string WEBHOOK_CALLBACK_VERIFICATION = "webhook_callback_verification";
    /// <summary>
    /// This type of webhook contains the reason why Twitch revoked your subscription.
    /// </summary>
    public const string REVOCATION = "revocation";
}
