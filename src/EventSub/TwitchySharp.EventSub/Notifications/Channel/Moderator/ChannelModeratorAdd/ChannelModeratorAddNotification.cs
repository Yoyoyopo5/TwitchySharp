namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelModeratorAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelmoderatoradd">Channel Moderator Add</see> for more information.
/// </remarks>
public record ChannelModeratorAddNotification : EventSubNotification<ChannelModeratorAddEvent, ChannelModeratorAddCondition>;
