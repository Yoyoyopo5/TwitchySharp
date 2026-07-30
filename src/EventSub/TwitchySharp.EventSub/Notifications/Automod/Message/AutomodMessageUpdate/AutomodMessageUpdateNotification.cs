namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageUpdate"/>
/// </summary>
/// <remarks>
/// <see cref="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessageupdate">Automod Message Update</see> for more information.
/// </remarks>
public record AutomodMessageUpdateNotification : EventSubNotification<AutomodMessageUpdateEvent, AutomodMessageUpdateCondition>;
