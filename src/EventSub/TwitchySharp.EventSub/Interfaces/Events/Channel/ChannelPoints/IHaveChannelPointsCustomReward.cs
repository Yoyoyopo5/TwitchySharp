using System.Text.Json.Serialization;
using TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;

/// <summary>
/// A Channel Points custom reward.
/// </summary>
public interface IHaveChannelPointsCustomReward
{
    /// <summary>
    /// The id of the reward.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// Indicates whether the reward is currently enabled.
    /// </summary>
    bool IsEnabled { get; }
    /// <summary>
    /// Indicates whether the reward is paused.
    /// </summary>
    bool IsPaused { get; }
    /// <summary>
    /// Indicates whether the reward is in stock.
    /// </summary>
    bool IsInStock { get; }
    /// <summary>
    /// The title of the reward.
    /// </summary>
    string Title { get; }
    /// <summary>
    /// The cost of redeeming the reward, in channel points.
    /// </summary>
    int Cost { get; }
    /// <summary>
    /// The reward's description.
    /// </summary>
    string Prompt { get; }
    /// <summary>
    /// Indicates whether user text input is required to redeem the reward.
    /// </summary>
    bool IsUserInputRequired { get; }
    /// <summary>
    /// Indicates whether redemptions should immediately be marked as fulfilled, skipping the manual request queue.
    /// </summary>
    bool ShouldRedemptionsSkipRequestQueue { get; }
    /// <summary>
    /// Setting controlling how many times the reward can be redeemed per stream.
    /// </summary>
    MaxPerStreamSetting MaxPerStream { get; }
    /// <summary>
    /// Setting controlling how many times each user can redeem the reward per stream.
    /// </summary>
    MaxPerUserPerStreamSetting MaxPerUserPerStream { get; }
    /// <summary>
    /// The background color of the reward, in hex with # prefix.
    /// </summary>
    string BackgroundColor { get; }
    /// <summary>
    /// The custom image for the reward.
    /// This is <see langword="null"/> if no custom image has been uploaded for the reward.
    /// </summary>
    ChannelPointsRewardImage? Image { get; }
    /// <summary>
    /// The default image for the reward.
    /// </summary>
    ChannelPointsRewardImage DefaultImage { get; }
    /// <summary>
    /// Setting controlling the minimum amount of time that must pass between each reward redemption. 
    /// </summary>
    GlobalCooldownSetting GlobalCooldown { get; }
    /// <summary>
    /// The date and time when the reward's cooldown will expire and it can be redeemed again.
    /// This is <see langword="null"/> if <see cref="GlobalCooldown"/> is not enabled or
    /// the reward is not on cooldown.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    DateTimeOffset? CooldownExpiresAt { get; }
    /// <summary>
    /// The amount of times the reward has been redeemed during the current livestream.
    /// This is <see langword="null"/> if the stream is not live or 
    /// <see cref="MaxPerStream"/> is not enabled.
    /// </summary>
    int? RedemptionsRedeemedCurrentStream { get; }
}
