namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSubscriptionGift"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsubscriptiongift">Channel Subscription Gift</see> for more information.
/// </remarks>
public record ChannelSubscriptionGiftNotification : EventSubNotification<ChannelSubscriptionGiftEvent, ChannelSubscriptionGiftCondition>;
