namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a bits badge tier upgrade notification.
/// </summary>
public record ChannelChatNotificationBitsBadgeTier
{
    /// <summary>
    /// The tier of the Bits badge (how many Bits are required to acheive it).
    /// For example, <c>100</c>, <c>1000</c>, <c>10000</c>, etc.
    /// </summary>
    public required int Tier { get; init; }
}
