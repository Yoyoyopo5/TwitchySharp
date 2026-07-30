namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodSettingsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodsettingsupdate">Automod Settings Update</see> for more information.
/// </remarks>
public record AutomodSettingsUpdateNotification : EventSubNotification<AutomodSettingsUpdateEvent, AutomodSettingsUpdateCondition>;
