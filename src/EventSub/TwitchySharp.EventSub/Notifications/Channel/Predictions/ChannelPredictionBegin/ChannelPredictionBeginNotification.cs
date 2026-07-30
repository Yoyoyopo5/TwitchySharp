namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPredictionLock"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpredictionbegin">Channel Prediction Begin</see> for more information.
/// </remarks>
public record ChannelPredictionBeginNotification : EventSubNotification<ChannelPredictionBeginEvent, ChannelPredictionBeginCondition>;
