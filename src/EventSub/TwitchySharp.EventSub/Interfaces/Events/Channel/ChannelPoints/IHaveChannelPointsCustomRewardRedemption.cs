using TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;

/// <summary>
/// A custom Channel Points reward redemption.
/// </summary>
public interface IHaveChannelPointsCustomRewardRedemption : IHaveChannelPointsRewardRedemption
{
    /// <summary>
    /// The message provided by the user when redeeming the reward.
    /// If not provided or the reward does not require input, this is <see cref="string.Empty"/>.
    /// </summary>
    string UserInput { get; }
    /// <summary>
    /// The status of the redemption.
    /// This defaults to <see cref="ChannelPointsCustomRewardRedemptionStatus.Unfulfilled"/>.
    /// </summary>
    ChannelPointsCustomRewardRedemptionStatus Status { get; }
    /// <summary>
    /// The custom reward that was redeemed.
    /// </summary>
    ChannelPointsCustomReward Reward { get; }
}
