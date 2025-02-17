using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications;
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

/// <summary>
/// Contains basic functionality for untyped notifications.
/// Use reflection, a switch expression (preferred), or properties of the <see cref="Subscription"/> to determine the underlying type of the notification.
/// </summary>
public interface IEventSubNotification
{
    /// <summary>
    /// Preliminary subscription information.
    /// Use this to determine the exact type and version of the notification.
    /// </summary>
    EventSubSubscription Subscription { get; }
}

/// <summary>
/// Internal use to make it easier to determine types of untyped notifications.
/// Deserialize initial notification JSON into this type to get the subscription type.
/// </summary>
/// <param name="Subscription">Contains information about the type of the notification.</param>
internal record EventSubNotification(EventSubSubscriptionType Subscription);
