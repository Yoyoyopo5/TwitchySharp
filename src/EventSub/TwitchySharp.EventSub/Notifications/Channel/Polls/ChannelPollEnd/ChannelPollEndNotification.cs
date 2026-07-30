namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPollEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpollend">Channel Poll End</see> for more information.
/// </remarks>
public record ChannelPollEndNotification : EventSubNotification<ChannelPollEndEvent, ChannelPollEndCondition>;
