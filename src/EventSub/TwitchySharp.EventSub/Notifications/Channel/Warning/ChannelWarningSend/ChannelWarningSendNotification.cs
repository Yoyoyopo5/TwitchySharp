namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelWarningSend"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelwarningsend">Channel Warning Send</see> for more information.
/// </remarks>
public record ChannelWarningSendNotification : EventSubNotification<ChannelWarningSendEvent, ChannelWarningSendCondition>;
