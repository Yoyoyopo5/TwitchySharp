namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific emote in a channel points reward redemption chat message.
/// </summary>
public record ChannelPointsRewardRedemptionMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required EmoteId Id { get; init; }
    /// <summary>
    /// The character index of the chat message where the emote begins.
    /// </summary>
    public required int Begin { get; init; }
    /// <summary>
    /// The character index of the chat message where the emote ends.
    /// </summary>
    public required int End { get; init; }
}
