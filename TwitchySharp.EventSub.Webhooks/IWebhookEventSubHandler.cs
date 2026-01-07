using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Webhooks.Responses;
using TwitchySharp.EventSub.Webhooks.WebhookMessageProcessors;

namespace TwitchySharp.EventSub.Webhooks;
/// <summary>
/// Implement this interface to define behavior for EventSub webhook messages.
/// </summary>
/// <remarks>
/// If you have no clue what you're doing, create a new class that implements this interface, 
/// define what you want to happen when each of the events happen using the included methods,
/// and register it as a service of <see cref="IWebhookEventSubHandler"/> (if using DI),
/// or pass an instance of it directly to an <see cref="IEventSubWebhookMessageProcessor"/> (use the <see cref="DefaultEventSubWebhookMessageProcessor"/> or create your own processor).
/// </remarks>
public interface IWebhookEventSubHandler
{
    /// <summary>
    /// This method is called when Twitch sends a callback verification request for a new subscription.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#responding-to-a-challenge-request">Responding to a Challenge Request</see> for more information.
    /// </summary>
    /// <remarks>
    /// The <see cref="IEventSubWebhookMessageProcessor"/> should handle the actual callback verification process.
    /// This method is simply to notify you that a new subscription is being verified.
    /// </remarks>
    /// <param name="newSubscription">The new subscription that is being verified.</param>
    /// <param name="challenge">The challenge that was sent in the callback verification request.</param>
    ValueTask OnCallbackVerification(EventSubSubscription newSubscription, string challenge, CancellationToken ct = default);
    /// <summary>
    /// This method is called when a subscription is revoked by Twitch.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#revoking-your-subscription">Revoking Your Subscription</see> for more information.
    /// </summary>
    /// <param name="revokedSubscription">The subscription that is being revoked.</param>
    ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default);
    /// <summary>
    /// This method is called when a notification is received for an active subscription.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#processing-an-event">Processing an Event</see> for more information.
    /// </summary>
    /// <param name="notification">
    /// The EventSub notification that was received from Twitch.
    /// You can use a switch expression or other pattern matching to disambiguate between different notification types.
    /// </param>
    ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default);
}
