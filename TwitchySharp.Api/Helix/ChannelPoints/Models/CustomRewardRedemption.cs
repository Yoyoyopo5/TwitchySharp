using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.ChannelPoints;
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
    [JsonConverter(typeof(JsonStringEnumConverter))]
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

/// <summary>
/// Possible statuses of custom reward redemptions.
/// </summary>
public enum RewardRedemptionStatus
{
    Cancelled, Fulfilled, Unfulfilled
}

/// <summary>
/// A custom reward that was redeemed.
/// </summary>
public record RedeemedReward
{
    /// <summary>
    /// The unique id of the reward.
    /// </summary>
    public required string Id { get; init; }
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
