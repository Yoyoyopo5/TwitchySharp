namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.HypeTrainProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelhype_trainprogress">Hype Train Progress V2</see> for more information.
/// </remarks>
public record HypeTrainProgressNotification : EventSubNotification<HypeTrainProgressEvent, HypeTrainProgressCondition>;
