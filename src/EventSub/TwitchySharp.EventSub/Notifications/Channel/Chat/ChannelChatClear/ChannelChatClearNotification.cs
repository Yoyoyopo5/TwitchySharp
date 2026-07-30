namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatClear"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatclear">Channel Chat Clear</see> for more information.
/// </remarks>
public record ChannelChatClearNotification : EventSubNotification<ChannelChatClearEvent, ChannelChatClearCondition>;
