using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardUpdate"/> event.
/// </summary>
public record ChannelPointsCustomRewardUpdateEvent
{
    /// <summary>
    /// The id of the reward.
    /// </summary>
    public required RewardId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the reward belongs to.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the reward belongs to.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the reward belongs to.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// Indicates whether the reward is currently enabled.
    /// </summary>
    /// <remarks>
    /// If <see langword="false"/>, the reward won't be displayed to users.
    /// </remarks>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// Indicates whether the reward is currently paused.
    /// </summary>
    /// <remarks>
    /// If <see langword="true"/>, viewers can't redeem the reward.
    /// </remarks>
    public required bool IsPaused { get; init; }
    /// <summary>
    /// Indicated whether the reward is in stock.
    /// </summary>
    /// <remarks>
    /// If <see langword="false"/>, viewers can't redeem the reward.
    /// </remarks>
    public required bool IsInStock { get; init; }
    /// <summary>
    /// The reward title.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The reward cost, in channel points.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// The description of the reward.
    /// </summary>
    // Is this documentation accurate? This is what Twitch reports, but it might be different.
    public required string Prompt { get; init; }
    /// <summary>
    /// Indicates whether the redeeming user must enter a message to redeem the reward.
    /// </summary>
    public required bool IsUserInputRequired { get; init; }
    /// <summary>
    /// Indicates whether redemptions should be set to <c>fulfilled</c> status immediately upon being redeemed.
    /// </summary>
    public required bool ShouldRedemptionsSkipRequestQueue { get; init; }
    /// <summary>
    /// The per stream maximum setting for the reward.
    /// </summary>
    public required MaxPerStreamSetting MaxPerStream { get; init; }
    /// <summary>
    /// The per user maximum setting for the reward.
    /// </summary>
    public required MaxPerUserPerStreamSetting MaxPerUserPerStream { get; init; }
    /// <summary>
    /// The background color of the reward.
    /// </summary>
    public required RgbColor BackgroundColor { get; init; }
    /// <summary>
    /// The set of custom images for the reward.
    /// </summary>
    /// <remarks>
    /// This can be <see langword="null"/> if the reward has no custom image.
    /// </remarks>
    public ChannelPointsRewardImage? Image { get; init; }
    /// <summary>
    /// The set of default images for the reward.
    /// </summary>
    public required ChannelPointsRewardImage DefaultImage { get; init; }
    /// <summary>
    /// The global cooldown setting for the reward.
    /// </summary>
    public required GlobalCooldownSetting GlobalCooldown { get; init; }
    /// <summary>
    /// The time when the reward's cooldown will end, or <see langword="null"/> if the reward has no cooldown.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? CooldownExpiresAt { get; init; }
    /// <summary>
    /// The number of times this reward has been redeemed during the current livestream.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the stream isn't live or <see cref="MaxPerStream"/> is not enabled.
    /// </remarks>
    public int? RedemptionsRedeemedCurrentStream { get; init; }
}
