namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.GoalProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalprogress">Goal Progress</see> for more information.
/// </remarks>
public record GoalProgressNotification : EventSubNotification<GoalProgressEvent, GoalProgressCondition>;
