namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSubscribe"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsubscribe">Channel Subscribe</see> for more information.
/// </remarks>
public record ChannelSubscribeNotification : EventSubNotification<ChannelSubscribeEvent, ChannelSubscribeCondition>;
