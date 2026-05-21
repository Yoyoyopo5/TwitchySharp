namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSuspiciousUserMessage"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelsuspicious_usermessage">Channel Suspicious User Message</see> for more information.
/// </remarks>
public record ChannelSuspiciousUserMessageNotification : EventSubNotification<ChannelSuspiciousUserMessageEvent, ChannelSuspiciousUserMessageCondition>;
