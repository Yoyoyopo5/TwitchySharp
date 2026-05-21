namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelWarningAcknowledgement"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelwarningacknowledge">Channel Warning Acknowledgement</see> for more information.
/// </remarks>
public record ChannelWarningAcknowledgementNotification : EventSubNotification<ChannelWarningAcknowledgementEvent, ChannelWarningAcknowledgementCondition>;
