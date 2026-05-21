namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatMessage"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatmessage">Channel Chat Message</see> for more information.
/// </remarks>
public record ChannelChatMessageNotification : EventSubNotification<ChannelChatMessageEvent, ChannelChatMessageCondition>;
