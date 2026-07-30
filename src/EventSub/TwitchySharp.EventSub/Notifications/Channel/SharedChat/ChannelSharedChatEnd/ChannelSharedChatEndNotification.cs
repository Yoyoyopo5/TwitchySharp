namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSharedChatSessionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshared_chatend">Channel Shared Chat End</see> for more information.
/// </remarks>
public record ChannelSharedChatEndNotification : EventSubNotification<ChannelSharedChatEndEvent, ChannelSharedChatEndCondition>;
