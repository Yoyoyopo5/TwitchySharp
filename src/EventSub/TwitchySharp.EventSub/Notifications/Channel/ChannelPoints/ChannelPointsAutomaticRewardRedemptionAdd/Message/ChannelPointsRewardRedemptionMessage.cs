namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a message submitted with a channel points reward redemption.
/// </summary>
public record ChannelPointsRewardRedemptionMessage
{
    public required string Text { get; init; }
    /// <summary>
    /// The emotes included in the chat message.
    /// </summary>
    public required ChannelPointsRewardRedemptionMessageEmote[] Emotes { get; init; }
}
