namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// An EventSub notification condition with a broadcaster user id and channel points reward id.
/// </summary>
public record BroadcasterRewardCondition : BroadcasterCondition
{
    /// <summary>
    /// The id of the channel points reward the notification is for.
    /// </summary>
    public RewardId? RewardId { get; init; }
}
