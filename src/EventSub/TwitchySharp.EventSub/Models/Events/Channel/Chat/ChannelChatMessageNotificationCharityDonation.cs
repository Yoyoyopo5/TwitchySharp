using TwitchySharp.Shared.Models;

namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a charity donation notification.
/// </summary>
public record ChannelChatMessageNotificationCharityDonation
{
    /// <summary>
    /// The name of the charity that was donated to.
    /// </summary>
    public required string CharityName { get; init; }
    /// <summary>
    /// The amount that was donated.
    /// </summary>
    public required CharityAmount Amount { get; init; }
}
