using System;

namespace TwitchySharp.Api.Helix.Goals;

/// <summary>
/// Contains information about a specific goal that a broadcaster has created.
/// </summary>
public record CreatorGoal
{
    /// <summary>
    /// The goal's id.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster who created the goal.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The display name of the broadcaster who created the goal.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster who created the goal.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The type of goal.
    /// </summary>
    public required CreatorGoalType Type { get; init; }
    /// <summary>
    /// A description of the goal. Is an empty string if not specified.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// The goal’s current value. The meaning of this depends on the <see cref="Type"/>.
    /// </summary>
    public required int CurrentAmount { get; init; }
    /// <summary>
    /// The goal's target value. The meaning of this depends on the <see cref="Type"/>.
    /// </summary>
    public required int TargetAmount { get; init; }
    /// <summary>
    /// The date and time when the broadcaster created the goal.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
