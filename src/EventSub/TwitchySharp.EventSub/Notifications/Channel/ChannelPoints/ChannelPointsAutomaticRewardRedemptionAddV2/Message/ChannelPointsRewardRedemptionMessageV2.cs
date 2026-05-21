namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about the message submitted with a specific reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2
{
    public required string Text { get; init; }
    /// <summary>
    /// The message fragments.
    /// </summary>
    public required ChannelPointsRewardRedemptionMessageV2Fragment[] Fragments { get; init; }
}
