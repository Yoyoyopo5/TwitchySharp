namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSharedChatSessionUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshared_chatupdate">Channel Shared Chat Update</see> for more information.
/// </remarks>
public record ChannelSharedChatUpdateNotification : EventSubNotification<ChannelSharedChatUpdateEvent, ChannelSharedChatUpdateCondition>;
