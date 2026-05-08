using System.Text.Json.Serialization;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.ChannelPoints;
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
public record ChannelPointsCustomRewardAddCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/> event.
/// </summary>
public record ChannelPointsCustomRewardAddEvent : IHaveChannelPointsCustomReward, IHaveBroadcaster
{
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
    public required bool IsEnabled { get; init; }
    public required bool IsPaused { get; init; }
    public required bool IsInStock { get; init; }
    public required string Title { get; init; }
    public required int Cost { get; init; }
    public required string Prompt { get; init; }
    public required bool IsUserInputRequired { get; init; }
    public required bool ShouldRedemptionsSkipRequestQueue { get; init; }
    public required MaxPerStreamSetting MaxPerStream { get; init; }
    public required MaxPerUserPerStreamSetting MaxPerUserPerStream { get; init; }
    public required string BackgroundColor { get; init; }
    public ChannelPointsRewardImage? Image { get; init; }
    public required ChannelPointsRewardImage DefaultImage { get; init; }
    public required GlobalCooldownSetting GlobalCooldown { get; init; }
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? CooldownExpiresAt { get; init; }
    public int? RedemptionsRedeemedCurrentStream { get; init; }
}
