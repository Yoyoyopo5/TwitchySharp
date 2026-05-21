namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains basic information about a specific channel points custom reward.
/// </summary>
public record ChannelPointsCustomReward
{
    /// <summary>
    /// The id of the custom reward.
    /// </summary>
    public required RewardId Id { get; init; }
    /// <summary>
    /// The name of the custom reward.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The cost to redeem the custom reward, in channel points.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// The custom reward description.
    /// </summary>
    public required string Prompt { get; init; }
}
