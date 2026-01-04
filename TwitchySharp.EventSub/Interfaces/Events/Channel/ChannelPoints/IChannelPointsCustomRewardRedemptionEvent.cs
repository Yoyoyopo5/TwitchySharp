using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;

/// <summary>
/// The interface for Channel Points Custom Reward Redemption events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd"/>, 
/// <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate"/>.
/// </remarks>
public interface IChannelPointsCustomRewardRedemptionEvent : IHaveBroadcaster, IHaveUser, IChannelPointsRewardRedemptionEvent
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
