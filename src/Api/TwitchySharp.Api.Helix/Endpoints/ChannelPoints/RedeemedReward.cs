
namespace TwitchySharp.Api.Helix.ChannelPoints;

/// <summary>
/// A custom reward that was redeemed.
/// </summary>
public record RedeemedReward
{
    /// <summary>
    /// The unique id of the reward.
    /// </summary>
    public required RewardId Id { get; init; }
    /// <summary>
    /// The title of the reward.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The prompt displayed to the viewer if user input is required.
    /// </summary>
    public required string Prompt { get; init; }
    /// <summary>
    /// The reward’s cost, in Channel Points.
    /// </summary>
    public long Cost { get; init; }
}
