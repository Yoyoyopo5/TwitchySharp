using TwitchySharp.EventSub.Enums.Events.Channel.Goals;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Goals;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Goals;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.GoalEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalend">Goal End</see> for more information.
/// </remarks>
public record GoalEndNotification : EventSubNotification<GoalEndEvent, GoalEndCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.GoalEnd"/>.
/// </summary>
public record GoalEndCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.GoalEnd"/> event.
/// </summary>
public record GoalEndEvent : IHaveGoal, IHaveBroadcaster
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
    /// <summary>
    /// Indicates whether the goal was achieved (i.e., the target amount was reached when the goal ended).
    /// </summary>
    public required bool IsAchieved { get; init; }
    /// <summary>
    /// The date and time when the goal was ended by the broadcaster.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
