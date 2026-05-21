namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatClearUserMessages"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatclear_user_messages">Channel Chat Clear User Messages</see> for more information.
/// </remarks>
public record ChannelChatClearUserMessagesNotification : EventSubNotification<ChannelChatClearUserMessagesEvent, ChannelChatClearUserMessagesCondition>;
