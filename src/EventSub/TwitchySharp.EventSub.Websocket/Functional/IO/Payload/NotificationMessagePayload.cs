using TwitchySharp.EventSub.Notifications;
using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Websocket.Functional;

/// <summary>
/// An EventSub websocket notification message payload.
/// </summary>
/// <remarks>
/// /// See <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events#notification-message">Notification Message</see> for more information.
/// </remarks>
[Wrapper<IEventSubNotification>]
public readonly partial record struct NotificationMessagePayload(IEventSubNotification Value);
