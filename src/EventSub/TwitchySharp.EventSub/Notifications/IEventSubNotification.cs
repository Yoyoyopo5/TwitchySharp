namespace TwitchySharp.EventSub.Notifications;

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
    IEventSubSubscription Subscription { get; }
    /// <summary>
    /// Contains information about the event that triggered the notification.
    /// </summary>
    object? Event { get; }
}
