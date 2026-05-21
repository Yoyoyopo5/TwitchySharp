namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelRaid"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelraid">Channel Raid</see> for more information.
/// </remarks>
public record ChannelRaidNotification : EventSubNotification<ChannelRaidEvent, ChannelRaidCondition>;
