using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Stream;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.StreamOffline"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#streamoffline">Stream Offline</see> for more information.
/// </remarks>
public record StreamOfflineNotification : EventSubNotification<StreamOfflineEvent, StreamOfflineCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.StreamOffline"/>.
/// </summary>
public record StreamOfflineCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.StreamOffline"/> event.
/// </summary>
public record StreamOfflineEvent : IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose stream went offline.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose stream went offline.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose stream went offline.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
}
