namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatSettingsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchat_settingsupdate">Channel Chat Settings Update</see> for more information.
/// </remarks>
public record ChannelChatSettingsUpdateNotification : EventSubNotification<ChannelChatSettingsUpdateEvent, ChannelChatSettingsUpdateCondition>;
