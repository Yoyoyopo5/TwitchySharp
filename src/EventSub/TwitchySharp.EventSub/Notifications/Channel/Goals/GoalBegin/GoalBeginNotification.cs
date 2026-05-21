namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.GoalBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalbegin">Goal Begin</see> for more information.
/// </remarks>
public record GoalBeginNotification : EventSubNotification<GoalBeginEvent, GoalBeginCondition>;
