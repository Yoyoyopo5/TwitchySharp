namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSharedChatSessionBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshared_chatbegin">Channel Shared Chat Begin</see> for more information.
/// </remarks>
public record ChannelSharedChatBeginNotification : EventSubNotification<ChannelSharedChatBeginEvent, ChannelSharedChatBeginCondition>;
