namespace TwitchySharp.EventSub.Models.Notifications;
/// <summary>
/// Base class for EventSub notifications.
/// Create derived record classes that set the type parameters to add new notification types.
/// </summary>
/// <typeparam name="TEvent">Type of the event property of the notification.</typeparam>
/// <typeparam name="TCondition">Type of the condition property of the subscription property of the notification.</typeparam>
public record EventSubNotification<TEvent, TCondition> : IEventSubNotification
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
    public required TEvent Event { get; init; }
}
