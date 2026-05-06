using TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;

namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about a specific automatic (built-in) channel points reward that was redeemed.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionReward
{
    /// <summary>
    /// The type of reward that was redeemed.
    /// </summary>
    public required ChannelPointsAutomaticRewardType Type { get; init; }
    /// <summary>
    /// The cost of the reward, in channel points.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// The emote associated with the reward redemption, if any.
    /// </summary>
    public ChannelPointsAutomaticRewardUnlockedEmote? UnlockedEmote { get; init; } // Need to see if this is populated on gigantify.
}
