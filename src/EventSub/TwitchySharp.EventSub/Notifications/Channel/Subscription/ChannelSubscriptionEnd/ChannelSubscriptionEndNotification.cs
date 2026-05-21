namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSubscriptionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsubscriptionend">Channel Subscription End</see> for more information.
/// </remarks>
public record ChannelSubscriptionEndNotification : EventSubNotification<ChannelSubscriptionEndEvent, ChannelSubscriptionEndCondition>;
