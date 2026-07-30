namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelupdate">Channel Update</see> for more information.
/// </remarks>
public record ChannelUpdateNotification : EventSubNotification<ChannelUpdateEvent, ChannelUpdateCondition>;
