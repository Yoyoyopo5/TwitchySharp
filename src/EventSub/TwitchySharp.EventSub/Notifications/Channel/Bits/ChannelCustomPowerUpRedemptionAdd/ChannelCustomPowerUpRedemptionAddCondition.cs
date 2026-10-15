namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelCustomPowerUpRedemptionAdd"/>.
/// </summary>
public record ChannelCustomPowerUpRedemptionAddCondition : BroadcasterCondition
{
    /// <summary>
    /// The id of the specific custom power-up the notifications are for.
    /// </summary>
    public CustomBitsPowerUpId? RewardId { get; init; }
}
