namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.GoalEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalend">Goal End</see> for more information.
/// </remarks>
public record GoalEndNotification : EventSubNotification<GoalEndEvent, GoalEndCondition>;
