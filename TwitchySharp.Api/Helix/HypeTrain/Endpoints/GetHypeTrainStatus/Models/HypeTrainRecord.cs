using System;

namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains information about a specific Hype Train record.
/// </summary>
public record HypeTrainRecord
{
    /// <summary>
    /// The level of the Hype Train.
    /// </summary>
    public required int Level { get; init; }
    /// <summary>
    /// The total amount of points contributed to the Hype Train.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The date and time when the Hype Train record was set.
    /// </summary>
    public required DateTimeOffset AchievedAt { get; init; }
}
