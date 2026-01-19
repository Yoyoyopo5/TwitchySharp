namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains data about a specific contribution to a Hype Train.
/// </summary>
public record HypeTrainContribution
{
    /// <summary>
    /// The total amount contributed.
    /// The exact meaning of this number depends on the value of <see cref="Type"/>:
    /// <list type="table">
    /// <item>
    ///     <term><see cref="HypeTrainContributionType.Bits"/></term>
    ///     <description>The number of bits used.</description>
    /// </item>
    /// <item>
    ///     <term><see cref="HypeTrainContributionType.Subs"/></term>
    ///     <description>Values of 500, 1000, or 2500 represent tier 1, 2, or 3 subscriptions, respectively.</description>
    /// </item>
    /// </list>
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The contribution method used.
    /// </summary>
    public required HypeTrainContributionType Type { get; init; }
    /// <summary>
    /// The user id of the user who made the contribution.
    /// </summary>
    public required string User { get; init; }
}
