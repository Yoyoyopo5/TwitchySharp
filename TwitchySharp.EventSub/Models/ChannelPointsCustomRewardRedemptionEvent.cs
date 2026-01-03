using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for Channel Points Custom Reward Redemption events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd"/>,
/// <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate"/>.
/// </remarks>
public record ChannelPointsCustomRewardRedemptionEvent
{
    /// <summary>
    /// The id of the redemption.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that redeemed the reward.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that redeemed the reward.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that redeemed the reward.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The message provided by the user when redeeming the reward.
    /// If not provided or the reward does not require input, this is <see cref="string.Empty"/>.
    /// </summary>
    public required string UserInput { get; init; }
    /// <summary>
    /// The status of the redemption.
    /// This defaults to <see cref="ChannelPointsCustomRewardRedemptionStatus.Unfulfilled"/>.
    /// </summary>
    public required ChannelPointsCustomRewardRedemptionStatus Status { get; init; }
    /// <summary>
    /// The reward that was redeemed.
    /// </summary>
    public required ChannelPointsCustomReward Reward { get; init; }
    /// <summary>
    /// The date and time when the redemption occurred.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }
}

/// <summary>
/// Contains static definitions for possible Channel Points Custom Reward Redemption statuses.
/// </summary>
/// <param name="Value">The string value of the redemption status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelPointsCustomRewardRedemptionStatus, string>))]
public record ChannelPointsCustomRewardRedemptionStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPointsCustomRewardRedemptionStatus Unknown { get; } = new("unknown");
    /// <summary>
    /// The redemption is waiting in the request queue.
    /// </summary>
    public static ChannelPointsCustomRewardRedemptionStatus Unfulfilled { get; } = new("unfulfilled");
    /// <summary>
    /// The redemption has been marked as completed by the broadcaster or a moderator in the request queue, OR
    /// The redemption skipped the request queue because <c>ShouldRedemptionsSkipRequestQueue</c> was
    /// enabled on the custom reward.
    /// </summary>
    public static ChannelPointsCustomRewardRedemptionStatus Fulfilled { get; } = new("fulfilled");
    /// <summary>
    /// The redemption was marked as cancelled by the broadcaster or a moderator in the request queue, and
    /// channel points were refunded to the redeemer.
    /// </summary>
    public static ChannelPointsCustomRewardRedemptionStatus Canceled { get; } = new("canceled");
}

/// <summary>
/// Contains basic information about a specific channel points custom reward.
/// </summary>
public record ChannelPointsCustomReward
{
    /// <summary>
    /// The id of the custom reward.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the custom reward.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The cost to redeem the custom reward, in channel points.
    /// </summary>
    public required int Cost { get; init; }
    /// <summary>
    /// The custom reward description.
    /// </summary>
    public required string Prompt { get; init; }
}
