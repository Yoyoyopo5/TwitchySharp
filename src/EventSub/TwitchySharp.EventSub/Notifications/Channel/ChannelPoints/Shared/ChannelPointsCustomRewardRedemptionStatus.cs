using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for possible Channel Points Custom Reward Redemption statuses.
/// </summary>
/// <param name="Value">The string value of the redemption status.</param>
[Wrapper<string>]
public readonly partial record struct ChannelPointsCustomRewardRedemptionStatus(string Value)
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
