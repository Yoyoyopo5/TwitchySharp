using TwitchySharp.EventSub.Enums.Events.Channel.HypeTrain;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.HypeTrain;

/// <summary>
/// Contains information about a specific Hype Train contributor.
/// </summary>
public record HypeTrainTopContributor : IHaveUser
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
    public required int Total { get; init; }
}
