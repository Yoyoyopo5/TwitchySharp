using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.EventSub;

/// <summary>
/// Base interface for EventSub handlers.
/// </summary>
public interface IEventSubHandler
{
    /// <summary>
    /// This method is called when a notification is received for an active subscription.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#processing-an-event">Processing an Event</see> for more information.
    /// </summary>
    /// <param name="notification">
    /// The EventSub notification that was received from Twitch.
    /// You can use a switch expression or other pattern matching to disambiguate between different notification types.
    /// </param>
    ValueTask OnNotified(IEventSubNotification notification, CancellationToken ct = default);
    /// <summary>
    /// This method is called when a subscription is revoked by Twitch.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-webhook-events/#revoking-your-subscription">Revoking Your Subscription</see> for more information.
    /// </summary>
    /// <param name="revokedSubscription">The subscription that is being revoked.</param>
    ValueTask OnSubscriptionRevoked(EventSubSubscription revokedSubscription, CancellationToken ct = default);

    /// <summary>
    /// An error occurred during message processing.
    /// </summary>
    /// <param name="error">The error that occurred.</param>
    ValueTask OnError(Error error, CancellationToken ct = default);
}
