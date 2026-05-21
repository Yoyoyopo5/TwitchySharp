namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific Hype Train contributor.
/// </summary>
public record HypeTrainTopContributor
{
    /// <summary>
    /// The user id of the contributor.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the contributor.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the contributor.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The contribution type used.
    /// </summary>
    public required HypeTrainContributionType Type { get; init; }
    /// <summary>
    /// The total amount contributed by this user to the Hype Train.
    /// </summary>
    /// <remarks>
    /// If <see cref="Type"/> is <see cref="HypeTrainContributionType.Bits"/>, total represents the amount of Bits used. 
    /// If <see cref="Type"/> is <see cref="HypeTrainContributionType.Subscription"/>, total is 500, 1000, or 2500 to represent tier 1, 2, or 3 subscriptions, respectively.
    /// </remarks>
    public required HypeTrainPointCount Total { get; init; }
}
