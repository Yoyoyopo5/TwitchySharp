namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPredictionLock"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpredictionlock">Channel Prediction Lock</see> for more information.
/// </remarks>
public record ChannelPredictionLockNotification : EventSubNotification<ChannelPredictionLockEvent, ChannelPredictionLockCondition>;
