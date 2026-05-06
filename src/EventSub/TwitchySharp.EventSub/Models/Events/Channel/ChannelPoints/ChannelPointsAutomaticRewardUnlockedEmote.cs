namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about a specific emote unlocked from an automatic (built-in) channel points reward.
/// </summary>
public record ChannelPointsAutomaticRewardUnlockedEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the emote.
    /// </summary>
    public required string Name { get; init; }
}
