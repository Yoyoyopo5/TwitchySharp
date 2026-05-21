namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelVIPRemove"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelvipremove">Channel VIP Remove</see> for more information.
/// </remarks>
public record ChannelVipRemoveNotification : EventSubNotification<ChannelVipRemoveEvent, ChannelVipRemoveCondition>;
