namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelFollow"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelfollow">Channel Follow</see> for more information.
/// </remarks>
public record ChannelFollowNotification : EventSubNotification<ChannelFollowEvent, ChannelFollowCondition>;
