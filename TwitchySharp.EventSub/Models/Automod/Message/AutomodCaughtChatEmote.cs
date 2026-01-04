namespace TwitchySharp.EventSub.Models.Automod.Message;

/// <summary>
/// Contains information about a specific chat emote that triggered Automod.
/// </summary>
public record AutomodCaughtChatEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The id of the emote set that the emote belongs to.
    /// </summary>
    public required string EmoteSetId { get; init; }
}
