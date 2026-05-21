namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelBitsUse"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelbitsuse">Channel Bits Use</see> for more information.
/// </remarks>
public record ChannelBitsUseNotification : EventSubNotification<ChannelBitsUseEvent, ChannelBitsUseCondition>;
