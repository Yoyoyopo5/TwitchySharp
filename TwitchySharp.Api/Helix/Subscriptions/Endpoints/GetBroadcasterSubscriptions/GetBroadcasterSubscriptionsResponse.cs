namespace TwitchySharp.Api.Helix.Subscriptions;
/// <summary>
/// Contains a list of a specific broadcaster's subscribers.
/// </summary>
public record GetBroadcasterSubscriptionsResponse
{
    /// <summary>
    /// The list of subscribers.
    /// </summary>
    public required BroadcasterSubscriber[] Data { get; init; }
    /// <inheritdoc cref="Api.Pagination"/>
    public required Pagination Pagination { get; init; }
    /// <summary>
    /// The total number of users that subscribe to this broadcaster.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The current number of subscriber points earned by this broadcaster. 
    /// Points are based on the subscription tier of each user that subscribes to this broadcaster. 
    /// For example, a Tier 1 subscription is worth 1 point, Tier 2 is worth 2 points, and Tier 3 is worth 6 points.
    /// </summary>
    public required int Points { get; init; }
}