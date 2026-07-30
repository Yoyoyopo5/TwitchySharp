namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelVipAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelvipadd">Channel VIP Add</see> for more information.
/// </remarks>
public record ChannelVipAddNotification : EventSubNotification<ChannelVipAddEvent, ChannelVipAddCondition>;
