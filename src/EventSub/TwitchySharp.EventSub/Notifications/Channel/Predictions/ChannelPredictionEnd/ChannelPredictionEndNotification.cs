namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPredictionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpredictionend">Channel Prediction End</see> for more information.
/// </remarks>
public record ChannelPredictionEndNotification : EventSubNotification<ChannelPredictionEndEvent, ChannelPredictionEndCondition>;
