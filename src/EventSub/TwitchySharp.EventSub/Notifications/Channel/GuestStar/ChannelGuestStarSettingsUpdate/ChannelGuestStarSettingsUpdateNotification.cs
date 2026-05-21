namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarSettingsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_settingsupdate">Channel Guest Star Settings Update</see> for more information.
/// </remarks>
public record ChannelGuestStarSettingsUpdateNotification : EventSubNotification<ChannelGuestStarSettingsUpdateEvent, ChannelGuestStarSettingsUpdateCondition>;
