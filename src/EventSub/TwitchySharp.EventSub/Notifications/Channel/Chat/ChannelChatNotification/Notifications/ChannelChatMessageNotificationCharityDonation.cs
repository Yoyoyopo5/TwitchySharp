namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a charity donation notification.
/// </summary>
public record ChannelChatMessageNotificationCharityDonation
{
    /// <summary>
    /// The name of the charity that was donated to.
    /// </summary>
    public required CharityName CharityName { get; init; }
    /// <summary>
    /// The amount that was donated.
    /// </summary>
    public required CharityAmount Amount { get; init; }
}
