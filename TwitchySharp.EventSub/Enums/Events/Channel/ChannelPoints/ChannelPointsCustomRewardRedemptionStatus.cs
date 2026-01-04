using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;

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
