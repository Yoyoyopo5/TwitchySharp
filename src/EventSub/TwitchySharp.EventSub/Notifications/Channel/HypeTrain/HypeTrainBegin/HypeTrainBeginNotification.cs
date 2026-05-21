namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.HypeTrainBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelhype_trainbegin">Hype Train Begin V2</see> for more information.
/// </remarks>
public record HypeTrainBeginNotification : EventSubNotification<HypeTrainBeginEvent, HypeTrainBeginCondition>;
