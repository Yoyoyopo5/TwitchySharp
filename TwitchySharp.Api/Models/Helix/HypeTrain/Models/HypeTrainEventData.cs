using System;
using TwitchySharp.Api.Models.Helix.HypeTrain.Enums;

namespace TwitchySharp.Api.Models.Helix.HypeTrain.Models;

/// <summary>
/// Contains information about a specific Hype Train progression event.
/// </summary>
public record HypeTrainEventData
{
    /// <summary>
    /// The user id of the broadcaster that has the Hype Train.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The date and time that another Hype Train can start.
    /// </summary>
    public required DateTimeOffset CooldownEndTime { get; init; }
    /// <summary>
    /// The date and time when this Hype Train will end.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
    /// <summary>
    /// The value needed to reach the next level.
    /// Each contribution has a <see cref="HypeTrainContribution.Total">value</see>.
    /// </summary>
    public required int Goal { get; init; }
    /// <summary>
    /// The id of the Hype Train.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The most recent contribution to the Hype Train.
    /// </summary>
    public required HypeTrainContribution LastContribution { get; init; }
    /// <summary>
    /// The highest level that the Hype Train has reached.
    /// Levels are from 1-5.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The date and time when the Hype Train started.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The top contributors to the Hype Train.
    /// One contribution is listed for each <see cref="HypeTrainContributionType"/>.
    /// For example, the top contributor using <see cref="HypeTrainContributionType.Bits"/> (by aggregate) and the top contributor using <see cref="HypeTrainContributionType.Subs"/> (by count).
    /// </summary>
    public required HypeTrainContribution[] TopContributions { get; init; }
    /// <summary>
    /// The current total amount raised.
    /// This value is aggregated from each <see cref="HypeTrainContribution.Total"/> to the Hype Train.
    /// </summary>
    public required int Total { get; init; }
}