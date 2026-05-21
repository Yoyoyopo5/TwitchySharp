namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSubscriptionMessage"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsubscriptionmessage">Channel Subscription Message</see> for more information.
/// </remarks>
public record ChannelSubscriptionMessageNotification : EventSubNotification<ChannelSubscriptionMessageEvent, ChannelSubscriptionMessageCondition>;
