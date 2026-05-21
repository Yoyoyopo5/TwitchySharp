namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelCheer"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelcheer">Channel Cheer</see> for more information.
/// </remarks>
public record ChannelCheerNotification : EventSubNotification<ChannelCheerEvent, ChannelCheerCondition>;
