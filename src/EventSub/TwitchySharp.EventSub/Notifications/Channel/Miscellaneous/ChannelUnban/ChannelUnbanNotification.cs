namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUnban"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelunban">Channel Unban</see> for more information.
/// </remarks>
public record ChannelUnbanNotification : EventSubNotification<ChannelUnbanEvent, ChannelUnbanCondition>;
