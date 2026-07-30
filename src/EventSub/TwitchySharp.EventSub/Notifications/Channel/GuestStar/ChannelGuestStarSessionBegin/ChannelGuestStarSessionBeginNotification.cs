namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarSessionBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_sessionbegin">Channel Guest Star Session Begin</see> for more information.
/// </remarks>
public record ChannelGuestStarSessionBeginNotification : EventSubNotification<ChannelGuestStarSessionBeginEvent, ChannelGuestStarSessionBeginCondition>;
