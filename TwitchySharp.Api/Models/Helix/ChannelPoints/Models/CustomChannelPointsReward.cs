using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Models;
public record CustomChannelPointsReward
{
    /// <summary>
    /// The user id of the broadcaster who has this reward.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster who has this reward.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster who has this reward.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// An id that uniquely indentifies this custom reward.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The title of the reward.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The prompt shown to the viewer when they redeem the reward if user input is required (<see cref="IsUserInputRequired"/> is <see langword="true"/>).
    /// </summary>
    public required string Prompt { get; init; }
    /// <summary>
    /// The cost of the reward in Channel Points.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// A set of custom images for the reward. 
    /// This field is set to <see langword="null"/> if the broadcaster didn’t upload images.
    /// </summary>
    public RewardImage? Image { get; init; }
    /// <summary>
    /// A set of default images for the reward.
    /// </summary>
    public required RewardImage DefaultImage { get; init; }
    /// <summary>
    /// The background color of the reward. 
    /// The color is in Hex format (for example, #00E5CB).
    /// </summary>
    public required string BackgroundColor { get; init; }
    /// <summary>
    /// Determines whether the reward is enabled. 
    /// Is <see langword="true"/> if enabled; otherwise, <see langword="false"/>. 
    /// Disabled rewards aren’t shown to users.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// Determines whether the user must enter information when redeeming the reward. 
    /// Is <see langword="true"/> if the reward requires user input.
    /// </summary>
    public required bool IsUserInputRequired { get; init; }
    /// <summary>
    /// The settings used to determine whether to apply a maximum to the number to the redemptions allowed per live stream.
    /// </summary>
    public required MaxPerStreamSetting MaxPerStreamSetting { get; init; }
    /// <summary>
    /// The settings used to determine whether to apply a maximum to the number of redemptions allowed per user per live stream.
    /// </summary>
    public required MaxPerUserPerStreamRewardSetting MaxPerUserPerStreamSetting { get; init; }
    /// <summary>
    /// The settings used to determine whether to apply a cooldown period between redemptions and the length of the cooldown.
    /// </summary>
    public required GlobalCooldownSetting GlobalCooldownSetting { get; init; }
    /// <summary>
    /// Determines whether the reward is currently paused. 
    /// Is <see langword="true"/> if the reward is paused. 
    /// Viewers can’t redeem paused rewards.
    /// </summary>
    public required bool IsPaused { get; init; }
    /// <summary>
    /// Determines whether the reward is currently in stock. 
    /// Is <see langword="true"/> if the reward is in stock. 
    /// Viewers can’t redeem out of stock rewards.
    /// </summary>
    public required bool IsInStock { get; init; }
    /// <summary>
    /// Determines whether redemptions should be set to FULFILLED status immediately when a reward is redeemed. 
    /// If <see langword="true"/>, status is UNFULFILLED and follows the normal request queue process.
    /// </summary>
    public required bool ShouldRedemptionsSkipRequestQueue { get; init; }
    /// <summary>
    /// The number of redemptions redeemed during the current live stream. 
    /// The number counts against the <see cref="MaxPerStreamSetting"/> limit. 
    /// This field is <see langword="null"/> if the broadcaster’s stream isn’t live or <see cref="MaxPerStreamSetting"/> isn’t enabled.
    /// </summary>
    public int? RedemptionsRedeemedCurrentStream { get; init; }
    /// <summary>
    /// The time when the cooldown period expires. 
    /// Is <see langword="null"/> if the reward isn’t in a cooldown state.
    /// </summary>
    public DateTimeOffset? CooldownExpiresAt { get; init; }

}