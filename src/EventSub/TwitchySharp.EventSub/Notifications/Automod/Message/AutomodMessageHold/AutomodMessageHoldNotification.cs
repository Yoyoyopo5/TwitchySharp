namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.AutomodMessageHold"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#automodmessagehold">Automod Message Hold</see> for more information.
/// </remarks>
public record AutomodMessageHoldNotification : EventSubNotification<AutomodMessageHoldEvent, AutomodMessageHoldCondition>;
