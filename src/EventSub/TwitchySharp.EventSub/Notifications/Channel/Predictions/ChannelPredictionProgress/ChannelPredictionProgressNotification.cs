namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPredictionProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpredictionprogress">Channel Prediction Progress</see> for more information.
/// </remarks>
public record ChannelPredictionProgressNotification : EventSubNotification<ChannelPredictionProgressEvent, ChannelPredictionProgressCondition>;
