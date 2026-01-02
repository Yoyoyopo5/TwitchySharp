using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.EventSub.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_rewardadd">Channel Points Custom Reward Add</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardAddNotification : EventSubNotification<ChannelPointsCustomRewardAddEvent, ChannelPointsCustomRewardAddCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/>.
/// </summary>
public record ChannelPointsCustomRewardAddCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Channel Points Custom Reward Add notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/> event.
/// </summary>
public record ChannelPointsCustomRewardAddEvent
{
    /// <summary>
    /// The id of the reward.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the reward belongs to.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the reward belongs to.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the reward belongs to.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// Indicates whether the reward is currently enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// Indicates whether the reward is paused.
    /// </summary>
    public required bool IsPaused { get; init; }
    /// <summary>
    /// Indicates whether the reward is in stock.
    /// </summary>
    public required bool IsInStock { get; init; }
    /// <summary>
    /// The title of the reward.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The cost of redeeming the reward, in channel points.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// The reward's description.
    /// </summary>
    public required string Prompt { get; init; }
    /// <summary>
    /// Indicates whether user text input is required to redeem the reward.
    /// </summary>
    public required bool IsUserInputRequired { get; init; }
    /// <summary>
    /// Indicates whether redemptions should immediately be marked as fulfilled, skipping the manual request queue.
    /// </summary>
    public required bool ShouldRedemptionsSkipRequestQueue { get; init; }
    /// <summary>
    /// Setting controlling how many times the reward can be redeemed per stream.
    /// </summary>
    public required MaxPerStreamSetting MaxPerStream { get; init; }
    /// <summary>
    /// Setting controlling how many times each user can redeem the reward per stream.
    /// </summary>
    public required MaxPerUserPerStreamSetting MaxPerUserPerStream { get; init; }
    /// <summary>
    /// The background color of the reward, in hex with # prefix.
    /// </summary>
    public required string BackgroundColor { get; init; }
    /// <summary>
    /// The custom image for the reward.
    /// This is <see langword="null"/> if no custom image has been uploaded for the reward.
    /// </summary>
    public ChannelPointsRewardImage? Image { get; init; }
    /// <summary>
    /// The default image for the reward.
    /// </summary>
    public required ChannelPointsRewardImage DefaultImage { get; init; }
    /// <summary>
    /// Setting controlling the minimum amount of time that must pass between each reward redemption. 
    /// </summary>
    public required GlobalCooldownSetting GlobalCooldown { get; init; }
    /// <summary>
    /// The date and time when the reward's cooldown will expire and it can be redeemed again.
    /// This is <see langword="null"/> if <see cref="GlobalCooldown"/> is not enabled or
    /// the reward is not on cooldown.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? CooldownExpiresAt { get; init; }
    /// <summary>
    /// The amount of times the reward has been redeemed during the current livestream.
    /// This is <see langword="null"/> if the stream is not live or 
    /// <see cref="MaxPerStream"/> is not enabled.
    /// </summary>
    public int? RedemptionsRedeemedCurrentStream { get; init; }
}

/// <summary>
/// Contains information about a specific channel points reward max per stream setting.
/// </summary>
public record MaxPerStreamSetting
{
    /// <summary>
    /// Indicates whether the max per stream setting is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum per stream limit.
    /// </summary>
    public required int Value { get; init; }
}

/// <summary>
/// Contains information about a specific channel points reward max per user per stream setting.
/// </summary>
public record MaxPerUserPerStreamSetting
{
    /// <summary>
    /// Indicates whether the max per user per stream setting is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum per user per stream limit.
    /// </summary>
    public required int Value { get; init; }
}

/// <summary>
/// Contains URLs pointing to a specific channel point reward's image.
/// </summary>
public record ChannelPointsRewardImage
{
    /// <summary>
    /// URL for the image at 1x size (28x28).
    /// </summary>
    public required string Url1x { get; init; }
    /// <summary>
    /// URL for the image at 2x size (56x56).
    /// </summary>
    public required string Url2x { get; init; }
    /// <summary>
    /// URL for the image at 4x size (112x112).
    /// </summary>
    public required string Url4x { get; init; }
}

/// <summary>
/// Contains information about a specific channel point reward's global cooldown setting.
/// </summary>
public record GlobalCooldownSetting
{
    /// <summary>
    /// Indicates whether global cooldown is enabled for the reward.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The duration of the cooldown.
    /// This amount of time must elapse after a redemption before the reward can be redeemed again by any user.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("seconds")]
    public required TimeSpan Duration { get; init; }
}
