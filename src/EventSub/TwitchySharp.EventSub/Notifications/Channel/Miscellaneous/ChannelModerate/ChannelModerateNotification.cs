namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModerate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderate">Channel Moderate</see> for more information.
/// </remarks>
public record ChannelModerateNotification : EventSubNotification<ChannelModerateEvent, ChannelModerateCondition>;
