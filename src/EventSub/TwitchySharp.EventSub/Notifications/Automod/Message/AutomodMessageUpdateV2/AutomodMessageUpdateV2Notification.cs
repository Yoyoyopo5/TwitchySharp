namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageUpdateV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessageupdate-v2">Automod Message Update V2</see> for more information.
/// </remarks>
public record AutomodMessageUpdateV2Notification : EventSubNotification<AutomodMessageUpdateV2Event, AutomodMessageUpdateV2Condition>;
