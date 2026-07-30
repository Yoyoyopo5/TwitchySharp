namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModeratorRemove"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderatorremove">Channel Moderator Remove</see> for more information.
/// </remarks>
public record ChannelModeratorRemoveNotification : EventSubNotification<ChannelModeratorRemoveEvent, ChannelModeratorRemoveCondition>;
