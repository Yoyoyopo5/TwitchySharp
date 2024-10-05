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
    [JsonInclude, JsonRequired]
    public BitsLeaderboardData[] Data { get; private set; } = [];
    /// <summary>
    /// The reporting window’s start and end dates. 
    /// The dates are calculated by using the StartedAt and Period request parameters. 
    /// If you didn't specify the StartedAt query parameter, the dates are null.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateTimeOffsetRange DateRange { get; private set; }
    /// <summary>
    /// The number of ranked users in <see cref="Data"/>.
    /// This is the value in the Count request parameter or the total number of entries on the leaderboard, whichever is less.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int Total { get; private set; }
}

/// <summary>
/// Contains information about a single entry (user) on a bits leaderboard.
/// </summary>
public record BitsLeaderboardData
{
    /// <summary>
    /// The user's user id.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserId { get; private set; } = string.Empty;
    /// <summary>
    /// The user’s login name (username).
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserLogin { get; private set; } = string.Empty;
    /// <summary>
    /// The user’s display name.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserName { get; private set; } = string.Empty;
    /// <summary>
    /// The user’s position on the leaderboard.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int Rank { get; private set; }
    /// <summary>
    /// The number of Bits the user has cheered.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int Score { get; private set; }
}