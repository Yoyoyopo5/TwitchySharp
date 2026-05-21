namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.StreamOnline"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#streamonline">Stream Online</see> for more information.
/// </remarks>
public record StreamOnlineNotification : EventSubNotification<StreamOnlineEvent, StreamOnlineCondition>;
