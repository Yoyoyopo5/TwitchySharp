namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatUserMessageUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatuser_message_update">Channel Chat User Message Update</see> for more information.
/// </remarks>
public record ChannelChatUserMessageUpdateNotification : EventSubNotification<ChannelChatUserMessageUpdateEvent, ChannelChatUserMessageUpdateCondition>;
