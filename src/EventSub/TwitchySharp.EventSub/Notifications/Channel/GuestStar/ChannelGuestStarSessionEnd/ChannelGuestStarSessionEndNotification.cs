namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarSessionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_sessionend">Channel Guest Star Session End</see> for more information.
/// </remarks>
public record ChannelGuestStarSessionEndNotification : EventSubNotification<ChannelGuestStarSessionEndEvent, ChannelGuestStarSessionEndCondition>;
