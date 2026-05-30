using TwitchySharp.EventSub.Notifications;

namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// An EventSub websocket notification message payload.
/// </summary>
/// <remarks>
/// /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events#notification-message">Notification Message</see> for more information.
/// </remarks>
public readonly record struct NotificationMessagePayload
{
    public required IEventSubNotification Notification { get; init; }
}
