using TwitchySharp.EventSub.Enums.Events.Channel.Goals;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Goals;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Goals;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.GoalProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalprogress">Goal Progress</see> for more information.
/// </remarks>
public record GoalProgressNotification : EventSubNotification<GoalProgressEvent, GoalProgressCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.GoalProgress"/>.
/// </summary>
public record GoalProgressCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.GoalProgress"/> event.
/// </summary>
public record GoalProgressEvent : IHaveGoal, IHaveBroadcaster
{
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    public required ChannelGoalType Type { get; init; }
    public required string Description { get; init; }
    public required int CurrentAmount { get; init; }
    public required int TargetAmount { get; init; }
    public required DateTimeOffset StartedAt { get; init; }
}
