
namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Contains information about a single entry (user) on a bits leaderboard.
/// </summary>
public record BitsLeaderboardData
{
    /// <summary>
    /// The user's user id.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The user’s login name (username).
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The user’s display name.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The user’s position on the leaderboard.
    /// </summary>
    public required int Rank { get; init; }
    /// <summary>
    /// The number of Bits the user has cheered.
    /// </summary>
    public required int Score { get; init; }
}
