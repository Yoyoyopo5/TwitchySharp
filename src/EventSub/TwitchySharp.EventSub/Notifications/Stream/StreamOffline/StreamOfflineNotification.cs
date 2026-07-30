namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.StreamOffline"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#streamoffline">Stream Offline</see> for more information.
/// </remarks>
public record StreamOfflineNotification : EventSubNotification<StreamOfflineEvent, StreamOfflineCondition>;
