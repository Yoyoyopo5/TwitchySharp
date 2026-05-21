namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelChatUserMessageHold"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchatuser_message_hold">Channel Chat User Message Hold</see> for more information.
/// </remarks>
public record ChannelChatUserMessageHoldNotification : EventSubNotification<ChannelChatUserMessageHoldEvent, ChannelChatUserMessageHoldCondition>;
