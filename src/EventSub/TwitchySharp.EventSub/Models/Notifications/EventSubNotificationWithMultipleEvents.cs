
namespace TwitchySharp.EventSub.Models.Notifications;

/// <summary>
/// Base class for EventSub notifications that have multiple events.
/// </summary>
/// <remarks>
/// Right now, only <see cref="EventSubSubscriptionType.DropEntitlementGrant"/> has multiple events in its notification.
/// Annoying, but we need this type to support it.
/// </remarks>
/// <typeparam name="TEvent">Type of the event property of the notification.</typeparam>
/// <typeparam name="TCondition">Type of the condition property of the subscription property of the notification.</typeparam>
public record EventSubNotificationWithMultipleEvents<TEvent, TCondition> : IEventSubNotification
    where TEvent : class
    where TCondition : class
{
    /// <summary>
    /// Contains information about the subscription that the notification is for.
    /// </summary>
    public required EventSubSubscription<TCondition> Subscription { get; init; }
    /// <summary>
    /// <inheritdoc cref="IEventSubNotification.Subscription"/>
    /// </summary>
    EventSubSubscription IEventSubNotification.Subscription => Subscription;
    /// <summary>
    /// Contains information about the event that triggered the notification.
    /// </summary>
    public required TEvent[] Events { get; init; }
}
