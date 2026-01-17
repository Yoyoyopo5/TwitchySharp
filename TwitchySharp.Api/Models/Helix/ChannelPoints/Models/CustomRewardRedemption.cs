using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models.Helix.ChannelPoints.Enums;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Models;
/// <summary>
/// A specific custom reward redemption.
/// </summary>
public record CustomRewardRedemption
{
    /// <summary>
    /// The user id of the broadcaster that owns the custom reward.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The user login (username) of the broadcaster that owns the custom reward.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster that owns the custom reward.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// An id that uniquely identifies this redemption.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user login (username) of the user that redeemed the custom reward.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The user id of the user that redeemed the custom reward.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the user that redeemed the custom reward.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The text the user entered at the prompt when they redeemed the reward; otherwise, an empty string if user input was not required.
    /// </summary>
    public required string UserInput { get; init; }
    /// <summary>
    /// The state of the redemption.
    /// </summary>
    public required RewardRedemptionStatus Status { get; init; }
    /// <summary>
    /// The date and time of when the reward was redeemed.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }
    /// <summary>
    /// The reward that the user redeemed.
    /// </summary>
    public required RedeemedReward Reward { get; init; }
}