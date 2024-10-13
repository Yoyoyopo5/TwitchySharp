using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Contains information about the bits leaderboard for a broadcaster.
/// </summary>
public record GetBitsLeaderboardResponse
{
    /// <summary>
    /// A list of leaderboard leaders. 
    /// The leaders are returned in rank order by how much they’ve cheered. 
    /// The array is empty if nobody has cheered bits.
    /// </summary>
    public required BitsLeaderboardData[] Data { get; init; }
    /// <summary>
    /// The reporting window’s start and end dates. 
    /// The dates are calculated by using the StartedAt and Period request parameters. 
    /// If you didn't specify the StartedAt query parameter, the dates are null.
    /// </summary>
    public required DateTimeOffsetRange DateRange { get; init; }
    /// <summary>
    /// The number of ranked users in <see cref="Data"/>.
    /// This is the value in the Count request parameter or the total number of entries on the leaderboard, whichever is less.
    /// </summary>
    public required int Total { get; init; }
}

/// <summary>
/// Contains information about a single entry (user) on a bits leaderboard.
/// </summary>
public record BitsLeaderboardData
{
    /// <summary>
    /// The user's user id.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The user’s login name (username).
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The user’s display name.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user’s position on the leaderboard.
    /// </summary>
    public required int Rank { get; init; }
    /// <summary>
    /// The number of Bits the user has cheered.
    /// </summary>
    public required int Score { get; init; }
}