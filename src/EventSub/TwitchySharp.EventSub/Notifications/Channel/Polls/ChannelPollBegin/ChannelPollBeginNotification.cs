namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPollBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpollbegin">Channel Poll Begin</see> for more information.
/// </remarks>
public record ChannelPollBeginNotification : EventSubNotification<ChannelPollBeginEvent, ChannelPollBeginCondition>;
