namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.GoalBegin"/> event.
/// </summary>
public record GoalBeginEvent
{
    /// <summary>
    /// The id of the goal.
    /// </summary>
    public required ChannelGoalId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the goal.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The type of goal.
    /// </summary>
    public required ChannelGoalType Type { get; init; }
    /// <summary>
    /// The description of the goal, if specified by the broadcaster.
    /// </summary>
    /// <remarks>
    /// This can be a maximum of 40 characters.
    /// </remarks>
    public required string Description { get; init; }
    /// <summary>
    /// The current value of the goal.
    /// </summary>
    /// <remarks>
    /// The exact meaning of this integer is determined by <see cref="Type"/>.
    /// </remarks>
    public required int CurrentAmount { get; init; }
    /// <summary>
    /// The target value of the goal.
    /// </summary>
    public required int TargetAmount { get; init; }
    /// <summary>
    /// The date and time when the goal was created.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
