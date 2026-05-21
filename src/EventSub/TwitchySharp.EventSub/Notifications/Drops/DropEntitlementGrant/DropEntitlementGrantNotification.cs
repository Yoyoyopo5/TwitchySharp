namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.DropEntitlementGrant"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#dropentitlementgrant">Drop Entitlement Grant</see> for more information.
/// </remarks>
public record DropEntitlementGrantNotification : EventSubNotificationWithMultipleEvents<DropEntitlementGrantEvent, DropEntitlementGrantCondition>;
