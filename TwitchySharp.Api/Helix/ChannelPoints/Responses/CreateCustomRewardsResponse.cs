using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Contains a list of created custom rewards.
/// </summary>
public record CreateCustomRewardsResponse
{
    /// <summary>
    /// A list that contains the single custom reward you created.
    /// </summary>
    public CreatedCustomChannelPointsReward[] Data { get; private set; } = [];
}

public record CreatedCustomChannelPointsReward
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

/// <summary>
/// Contains image urls for channel point reward images.
/// </summary>
public record RewardImage
{
    /// <summary>
    /// The URL to a small version of the image.
    /// </summary>
    public required string Url1x { get; init; }
    /// <summary>
    /// The URL to a medium version of the image.
    /// </summary>
    public required string Url2x { get; init; }
    /// <summary>
    /// The URL to a large version of the image.
    /// </summary>
    public required string Url4x { get; init; }
}

/// <summary>
/// Controls the settings for how many times per stream a channel point reward can be redeemed.
/// </summary>
public record MaxPerStreamSetting
{
    /// <summary>
    /// Determines whether the reward applies a limit on the number of redemptions allowed per live stream. 
    /// Is <see langword="true"/> if the reward applies a limit.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum number of redemptions allowed per live stream.
    /// </summary>
    public required long MaxPerStream { get; init; }
}

/// <summary>
/// Controls the setting for how many time per stream an individual user can redeem a channel point reward.
/// </summary>
public record MaxPerUserPerStreamRewardSetting
{
    /// <summary>
    /// Determines whether the reward applies a limit on the number of redemptions allowed per user per live stream. 
    /// Is <see langword="true"/> if the reward applies a limit.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum number of redemptions allowed per user per live stream.
    /// </summary>
    public required long MaxPerUserPerStream { get; init; }
}

/// <summary>
/// Controls the setting for cooldown on a channel point reward.
/// </summary>
public record GlobalCooldownSetting
{
    /// <summary>
    /// Determines whether to apply a cooldown period. Is <see langword="true"/> if a cooldown period is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The cooldown period, in seconds.
    /// </summary>
    public required long GlobalCooldownSeconds { get; init; }
}
