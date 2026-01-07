using TwitchySharp.EventSub.Models;

namespace TwitchySharp.EventSub.Models.Notifications;

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
