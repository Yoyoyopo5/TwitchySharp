using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;

public interface IChannelPointsRewardRedemptionEvent
{
    /// <summary>
    /// The id of the redemption.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// The date and time when the reward was redeemed.
    /// </summary>
    DateTimeOffset RedeemedAt { get; }
}
