namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelAdBreakBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelad_breakbegin">Channel Ad Break Begin</see> for more information.
/// </remarks>
public record ChannelAdBreakBeginNotification : EventSubNotification<ChannelAdBreakBeginEvent, ChannelAdBreakBeginCondition>;
