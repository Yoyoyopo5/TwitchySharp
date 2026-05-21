namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSuspiciousUserUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsuspicious_userupdate">Channel Suspicious User Update</see> for more information.
/// </remarks>
public record ChannelSuspiciousUserUpdateNotification : EventSubNotification<ChannelSuspiciousUserUpdateEvent, ChannelSuspiciousUserUpdateCondition>;
