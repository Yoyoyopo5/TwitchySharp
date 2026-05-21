namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPollProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpollprogress">Channel Poll Progress</see> for more information.
/// </remarks>
public record ChannelPollProgressNotification : EventSubNotification<ChannelPollProgressEvent, ChannelPollProgressCondition>;
