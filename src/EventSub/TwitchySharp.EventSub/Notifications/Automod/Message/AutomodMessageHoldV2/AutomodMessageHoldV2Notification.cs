namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageHoldV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessagehold-v2">Automod Message Hold V2</see> for more information.
/// </remarks>
public record AutomodMessageHoldV2Notification : EventSubNotification<AutomodMessageHoldV2Event, AutomodMessageHoldV2Condition>;
