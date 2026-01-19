namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// An EventSub transport that uses <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/">webhooks</see>.
/// </summary>
/// <param name="Callback">
/// The url that subscription notifications will be sent to.
/// The callback must use SSL and listen on port 443.
/// </param>
/// <param name="Secret">
/// The secret used to verify the signature of the notification. 
/// The secret must be an ASCII string that’s a minimum of 10 characters long and a maximum of 100 characters long. 
/// For information about how the secret is used, see <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events#verifying-the-event-message">Verifying the event message</see>.
/// </param>
public sealed record WebhookSubscriptionTransport(string Callback, string Secret)
    : NewEventSubSubscriptionTransport
{
    /// <summary>
    /// The transport method identifier.
    /// </summary>
    public override string Method => "webhook";
    /// <summary>
    /// The url that subscription notifications will be sent to.
    /// </summary>
    public override string Callback { get; } = Callback;
    /// <summary>
    /// The secret used to verify the signature of the notification.
    /// For information about how the secret is used, see <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events#verifying-the-event-message">Verifying the event message</see>.
    /// </summary>
    public override string Secret { get; } = Secret;
}
