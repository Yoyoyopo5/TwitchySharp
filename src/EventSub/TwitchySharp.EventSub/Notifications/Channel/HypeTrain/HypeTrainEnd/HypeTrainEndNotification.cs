namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.HypeTrainEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelhype_trainend">Hype Train End V2</see> for more information.
/// </remarks>
public record HypeTrainEndNotification : EventSubNotification<HypeTrainEndEvent, HypeTrainEndCondition>;
