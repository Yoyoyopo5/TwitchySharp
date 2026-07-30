namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarGuestUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_guestupdate">Channel Guest Star Guest Update</see> for more information.
/// </remarks>
public record ChannelGuestStarGuestUpdateNotification : EventSubNotification<ChannelGuestStarGuestUpdateEvent, ChannelGuestStarGuestUpdateCondition>;
