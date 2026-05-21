namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific automatic (built-in) channel points reward that was redeemed.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionV2Reward
{
    /// <summary>
    /// The type of automatic (built-in) reward that was redeemed.
    /// </summary>
    public required ChannelPointsAutomaticRewardV2Type Type { get; init; }
    /// <summary>
    /// The number of channel points used to redeem the reward.
    /// </summary>
    public required int ChannelPoints { get; init; }
    /// <summary>
    /// The emote associated with the reward redemption, if any.
    /// </summary>
    public ChannelPointsAutomaticRewardUnlockedEmote? Emote { get; init; }
}
