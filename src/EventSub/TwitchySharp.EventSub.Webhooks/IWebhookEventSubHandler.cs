namespace TwitchySharp.EventSub.Webhooks;
/// <summary>
/// Implement this interface to define behavior for EventSub webhook messages.
/// </summary>
public interface IWebhookEventSubHandler : IEventSubHandler
{
    /// <summary>
    /// This method is called when Twitch sends a callback verification request for a new subscription.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#responding-to-a-challenge-request">Responding to a Challenge Request</see> for more information.
    /// </summary>
    /// <remarks>
    /// The <see cref="WebhookRequestProcessor"/> should handle the actual callback verification process.
    /// This method is simply to notify you that a new subscription is being verified.
    /// </remarks>
    /// <param name="newSubscription">The new subscription that is being verified.</param>
    /// <param name="challenge">The challenge that was sent in the callback verification request.</param>
    ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default);
}
