namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Contains a list of created custom rewards.
/// </summary>
public record CreateCustomRewardsResponseContent
{
    /// <summary>
    /// A list that contains the single custom reward you created.
    /// </summary>
    public required CustomChannelPointsReward[] Data { get; init; }
}
