namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelBan"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelban">Channel Ban</see> for more information.
/// </remarks>
public record ChannelBanNotification : EventSubNotification<ChannelBanEvent, ChannelBanCondition>;
