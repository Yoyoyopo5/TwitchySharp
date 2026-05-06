using System.Text.Json;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Notifications;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;
/// <summary>
/// A welcome message payload.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/handling-websocket-events#welcome-message">Welcome Message</see> for more information.
/// </remarks>
public class NotificationMessagePayload : IEventSubNotification
{
    /// <summary>
    /// The EventSub subscription.
    /// </summary>
    public required EventSubSubscription Subscription { get; init; }
    /// <summary>
    /// The EventSub notification.
    /// This must be further deserialized into a specific <see cref="IEventSubNotification"/> type.
    /// </summary>
    public required JsonElement Event { get; init; }
}
