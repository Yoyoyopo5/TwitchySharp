namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Contains a list of custom rewards.
/// </summary>
public record GetCustomRewardResponseContent
{
    /// <summary>
    /// A list of custom rewards. 
    /// The list is in ascending order by id. 
    /// If the broadcaster hasn’t created custom rewards, the list is empty.
    /// </summary>
    public required CustomChannelPointsReward[] Data { get; init; }
}
