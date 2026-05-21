namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatNotification"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatnotification">Channel Chat Notification</see> for more information.
/// </remarks>
public record ChannelChatNotificationNotification : EventSubNotification<ChannelChatNotificationEvent, ChannelChatNotificationCondition>;
