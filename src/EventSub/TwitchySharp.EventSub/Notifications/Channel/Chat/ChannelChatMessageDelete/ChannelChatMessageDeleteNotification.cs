namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatMessageDelete"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatmessage_delete">Channel Chat Message Delete</see> for more information.
/// </remarks>
public record ChannelChatMessageDeleteNotification : EventSubNotification<ChannelChatMessageDeleteEvent, ChannelChatMessageDeleteCondition>;
