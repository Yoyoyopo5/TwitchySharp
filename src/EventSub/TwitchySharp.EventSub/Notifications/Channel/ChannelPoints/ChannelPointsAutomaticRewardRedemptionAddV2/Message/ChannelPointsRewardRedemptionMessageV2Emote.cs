namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific emote used in a reward redemption message.
/// </summary>
public record ChannelPointsRewardRedemptionMessageV2Emote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The emote name.
    /// </summary>
    public required EmoteName Name { get; init; }
}
