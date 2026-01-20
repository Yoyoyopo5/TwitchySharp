namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains information about a specific Hype Train contribution.
/// </summary>
public record HypeTrainContribution
{
    /// <summary>
    /// The user id of the contributor.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the contributor.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the contributor.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The type of contribution.
    /// </summary>
    public required HypeTrainContributionType Type { get; init; }
    /// <summary>
    /// The total number of points contributed.
    /// </summary>
    public required int Total { get; init; }
}