namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModerateV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderate-v2">Channel Moderate V2</see> for more information.
/// </remarks>
public record ChannelModerateV2Notification : EventSubNotification<ChannelModerateV2Event, ChannelModerateV2Condition>;
